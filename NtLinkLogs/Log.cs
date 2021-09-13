using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.StorageClient;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System.IO;

namespace NtLinkLogs
{
    public partial class Log : Form
    {
        private readonly CultureInfo _cultureInfo;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly string _dateTimeFormat;
        string MonitorTimeStart;

        // This is our custom TextWriter class
        TextWriter _writer = null;

        public Log()
        {
            InitializeComponent();
            this._dateTimeFormat = "yyyy-MM-dd";
            this._cultureInfo = CultureInfo.InvariantCulture;
            this._timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
        }

        private void Log_Load(object sender, EventArgs e)
        {
            dtpQueryDate.CustomFormat = this._dateTimeFormat;
            // Instantiate the writer & redirect the out Console stream
            _writer = new StreamWriter(txtOutput);
            Console.SetOut(_writer);
            // Initialize controls
            txtQueryHoraInicial.Text = DateTime.Now.AddHours(-1).ToString("HH:mm");
            txtQueryHoraFinal.Text = DateTime.Now.ToString("HH:mm");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtQueryHoraInicial.Text.Length < 5)
            {
                MessageBox.Show("Ingrese una hora de inicio válida.");
                return;
            }
            if (txtQueryHoraFinal.Text.Length < 5)
            {
                MessageBox.Show("Ingrese una hora final válida.");
                return;
            }

            DateTime queryDate = dtpQueryDate.Value.ToUniversalTime();
            DateTime initialDate, finalDate;
            initialDate = DateTime.Parse(string.Format("{0} {1}:00", queryDate.ToString(this._dateTimeFormat), txtQueryHoraInicial.Text));
            finalDate = DateTime.Parse(string.Format("{0} {1}:00", queryDate.ToString(this._dateTimeFormat), txtQueryHoraFinal.Text));

            if (initialDate > finalDate)
            {
                MessageBox.Show("Ingrese horas de búsqueda válidas");
                return;
            }

            string initialHour = initialDate.ToUniversalTime().ToString("dd HH':'mm':'ss.fff");
            string finalHour = finalDate.ToUniversalTime().ToString("dd HH':'mm':'ss.fff");
            this.Query(queryDate, initialHour, finalHour);
        }

        private void Query(DateTime utcQueryDate, string initialValue, string finalValue)
        {
            var PartitionFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, utcQueryDate.ToString(this._dateTimeFormat));
            var RowKeyGreatFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, initialValue);
            var RowKeyLessFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, finalValue);
            var RowKeyFilter = TableQuery.CombineFilters(RowKeyGreatFilter, TableOperators.And, RowKeyLessFilter);

            var condCompl = TableQuery.CombineFilters(PartitionFilter, TableOperators.And, RowKeyFilter);

            var LogEntries = this.GetLogsFromTable(condCompl);
            dgvQueryResults.DataSource = LogEntries;
            dgvQueryResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private List<AzureLogEntity> GetLogsFromTable(string filter)
        {
            try
            {
                string storageAccountName = ConfigurationManager.AppSettings["StorageAccountName"];
                string storageAccountKey = ConfigurationManager.AppSettings["StorageAccountKey"];
                string storageTableName = ConfigurationManager.AppSettings["StorageTableName"];

                var account = new Microsoft.WindowsAzure.Storage.CloudStorageAccount(
                   new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(storageAccountName, storageAccountKey), true);

                var tableClient = account.CreateCloudTableClient();
                CloudTable table = tableClient.GetTableReference(storageTableName);

                var query = new TableQuery<AzureLogEntity>().Where(filter);

                var LogEntries = table.ExecuteQuery(query);
                LogEntries.ToList().Where(x => x.Message.Contains(""));

                return LogEntries.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al intenar obtener los registros del Log.");
                return new List<AzureLogEntity>();
            }
        }

        private void btnStartMonitor_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Inciando...");
            this.MonitorTimeStart = DateTime.Now.ToUniversalTime().AddMinutes(-15).ToString("dd HH':'mm':'ss.fff");
            btnStopMonitor.Enabled = true;
            btnStartMonitor.Enabled = false;
            tmrMonitorTicker.Start();
        }

        private void btnStopMonitor_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Deteniendo...");
            btnStopMonitor.Enabled = false;
            btnStartMonitor.Enabled = true;
            tmrMonitorTicker.Stop();
            Console.WriteLine("Monitor detenido.");
        }

        private void tmrMonitorTicker_Tick(object sender, EventArgs e)
        {
            // Obtenemos los registros
            var partitionFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, DateTime.Today.ToString(this._dateTimeFormat));
            var rowKeyGreatFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, MonitorTimeStart);
            var criteria = TableQuery.CombineFilters(partitionFilter, TableOperators.And, rowKeyGreatFilter);
            var logEntries = this.GetLogsFromTable(criteria);
            // Mostramos los registros
            foreach (var l in logEntries)
            {
                this.Invoke(new Action(() =>
                {
                    this.txtOutput.AppendText("\r\n" + l.PartitionKey + " - " + l.Message);
                }));
                //  Console.WriteLine();
            }

            if (logEntries.Count > 0)
            {
                var finalRow = logEntries.OrderByDescending(l => l.RowKey).First();
                var newMonitorTimeStart = DateTime.Parse(finalRow.RowKey.Substring(3, 12));
                // Desplaza la fecha un milisegundo
                newMonitorTimeStart = newMonitorTimeStart.AddMilliseconds(1);
                this.MonitorTimeStart = newMonitorTimeStart.ToString("dd HH':'mm':'ss.fff"); ;
            }
            else
            {
                if (txtOutput.Text.EndsWith("."))
                {
                    Console.Write(".");
                }
                else
                    Console.Write("\n.");
            }
        }

        private void dgvQueryResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvQueryResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                 var logEntry = (AzureLogEntity)this.dgvQueryResults.Rows[e.RowIndex].DataBoundItem;
                 
                if (logEntry.Message.Contains("ERROR"))
                    e.CellStyle.BackColor = Color.Red;

            }
        }

    }
}
