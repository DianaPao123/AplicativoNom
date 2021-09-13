using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;


namespace NtLinkLogs
{    
    class AzureLogEntity : TableEntity
    {
        public AzureLogEntity() { }
        public string DeploymentId { get; set; }
        public string Role { get; set; }
        public string RoleInstance { get; set; }
        public int Level { get; set; }
        public int EventId { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public Int64 EventTickCount { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            string res = "";
            foreach (var prop in this.GetType().GetProperties())
            {
                res += string.Format("{0} = {1}, ", prop.Name, prop.GetValue(this));
            }
            return res.Length > 0 ? res.Remove(res.Length - 1) : res;
            //return base.ToString();
        }

        public string GetRelData()
        {
            string res = "";

            res += "Message: " + this.Message + "\n";
            res += "Role: " + this.Role + "\n";
            res += "RoleInstance: " + this.RoleInstance + "\n";
            res += "Level: " + this.Level + "\n";
            res += "EventId: " + this.EventId + "\n";
            res += "Pid: " + this.Pid + "\n";
            res += "Tid: " + this.Tid + "";

            return res;
        }
    }
}
