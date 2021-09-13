<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="wfrLogin.aspx.cs" Inherits="GafLookPaid.wfrLogin" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  
    
  
    <style type="text/css">
        .auto-style2 {
            width: 387px;
        }
        .auto-style3 {
            width: 100px;
        }
        .auto-style4 {
            color: #990000;
        }
        .auto-style8 {
            width: 101px;
            height: 33px;
            text-align: center;
        }
        .auto-style9 {
            width: 387px;
            text-align: center;
        }
    </style>
  
    
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <link href="Styles/StyleBoton.css" rel="stylesheet" type="text/css" />
   	<div align="center">
	       <h1 class="auto-style1">Acceso Nomina</h1></br>
				<asp:Login ID="logMain" CssClass="login" runat="server" 
                LoginButtonText="Iniciar Sesión" LoginButtonType="Button" LoginButtonStyle-CssClass="btn btn-primary" BorderWidth="0px"
                TextLayout="TextOnTop" Width="353px"  Height="133px"
                DisplayRememberMe="False" FailureText="* Error de Inicio de Sesión."  
                PasswordLabelText="Contraseña" PasswordRequiredErrorMessage="*Requerido" TitleText="" 
                UserNameLabelText="Nombre de usuario:" UserNameRequiredErrorMessage="* Requerido" onauthenticate="logMain_Authenticate" BorderStyle= "solid"
                DestinationPageUrl="Default.aspx" style="text-align: center"  BorderColor="#89C221" ForeColor="#454444" border-shadow="2px" BackColor="#CDCECE" >
                          
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0" style="height:167px; width:353px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server" BorderWidth="1px" Width="80%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="UserName" ErrorMessage="* Requerido" ToolTip="* Requerido" 
                                                ValidationGroup="logMain">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server" BorderWidth="1px" TextMode="Password" Width="80%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                ControlToValidate="Password" ErrorMessage="*Requerido" ToolTip="*Requerido" 
                                                ValidationGroup="logMain">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="text-align: center">
                                            <asp:LinkButton ID="LoginLinkButton" runat="server" CommandName="Login" 
                                               CssClass="btn btn-primary"  ValidationGroup="logMain">Iniciar Sesión</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            
                <TextBoxStyle BorderWidth="1px" Width="80%" />
            </asp:Login>
		<br />
		      <asp:LinkButton runat="server" Text="Olvidé mi contraseña" ID="btnOlvidar1" 
            onclick="btnOlvidar_Click"/><br/>
            <a href="http://www.ntlink.com.mx/video1.html">Tutorial acceso NTLink</a>
         <div align="center" > 
                  
            </div>
		
	</div>
    <div runat="server" ID="divPasswordChange" Visible="False" align="center" >
        
        <table>
            <tr>
                <td class="auto-style8">
                    Email:
                    </td> <td class="auto-style9"><asp:TextBox runat="server" ID="txtOlvide" ValidationGroup="vgOlvide" Width="190px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOlvide" ID="valOlvide" ErrorMessage="Campo Obligatorio" Display="Dynamic" ValidationGroup="vgOlvide"></asp:RequiredFieldValidator>
                              
                          </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    RFC:                    
                </td>
                <td class="auto-style9">
                          <asp:TextBox runat="server" ID="txtRfcOlvide" Width="189px" />
                      <asp:RequiredFieldValidator runat="server" ID="valRfc" ControlToValidate="txtRfcOlvide" ErrorMessage="Campo Obligatorio" Display="Dynamic" ValidationGroup="vgOlvide"></asp:RequiredFieldValidator>
                </td>
            </tr>
            </br>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblMensajePass" Visible="False"></asp:Label>
                                   </td>
            </tr>
        </table>
        <asp:Button runat="server" ID="btnEnviarPass" Text="Enviar" ValidationGroup="vgOlvide" class="btn btn-primary"
            onclick="btnEnviarPass_Click" />
    </div>
	<asp:ModalPopupExtender runat="server" ID="mpeCambiarPassword" TargetControlID="btnPasswordDummy" BackgroundCssClass="mpeBack"
	 CancelControlID="btnCerrarPassword" PopupControlID="pnlCambiarPassword" />
	<asp:Panel runat="server" ID="pnlCambiarPassword" style="text-align: center;" Width="800px" BackColor="White">
		<h1>Cambiar Password</h1>
		<asp:Label runat="server" ID="lblUserIdCambiarPassword" Visible="False" />
		Es la primera vez que accedes al sitio, es necesario que cambies tu password.
		<table align="center">
			<tr>
				<td>Password:</td>
				<td>
					<asp:TextBox runat="server" ID="txtPassword" Width="250px" TextMode="Password" />
				</td>
			</tr>
			<tr>
				<td />
				<td>
					<asp:RequiredFieldValidator runat="server" ID="rfvPassword" ControlToValidate="txtPassword"
					 ErrorMessage="* Requerido" ValidationGroup="CambiarPassword" Display="Dynamic"  />
					<asp:RegularExpressionValidator runat="server" ID="revPassword" ControlToValidate="txtPassword"
					 Display="Dynamic" ErrorMessage="* El password no cumple con las politicas de seguridad"
					 ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%+-_]).{8,20})" ValidationGroup="CambiarPassword" />
					<asp:CompareValidator runat="server" ID="cvPassword" ControlToValidate="txtPassword" Display="Dynamic"
					 ControlToCompare="txtConfirmarPassword" ErrorMessage="* La confirmacion y el password no coinciden"
					  Operator="Equal" ValidationGroup="CambiarPassword" />
				</td>
			</tr>
			<tr>
				<td>Confirmar:</td>
				<td><asp:TextBox runat="server" ID="txtConfirmarPassword" TextMode="Password" Width="250px" /></td>
			</tr>
		</table>
        <br />
		<asp:Label ID="Label1" runat="server" Text="Label">Términos y Condiciones</asp:Label>
		<br />
        <center>
        <div>
<asp:Panel ID="pnldynamic" 
           runat="server" 
           BorderColor="" 
           BorderStyle="Solid" 
           BorderWidth="1px" 
           Height="150px" 
           ScrollBars="Auto" 
           Width="60%" 
           BackColor="" 
           Font-Names="Arial" 
           HorizontalAlign="Center" style="text-align: justify; font-size: x-small;" 
                Font-Size="XX-Small">
    <span class="style1"></span>
    CONTRATO DE PRESTACIÓN DE SERVICIOS QUE CELEBRAN, POR UNA PARTE NT LINK COMUNICACIONES, S.A. DE C.V., A QUIEN EN LO
    SUCESIVO SE LE DENOMINARÁ DE FORMA INDISTINTA “EL PRESTADOR” O “NT LINK” Y POR LA OTRA EL USUARIO SUSCRIPTOR DEL
    SERVICIO DE TIMBRADO Y/O GENERACIÓN DE CFDI, POR SU PROPIO DERECHO, A QUIEN EN LO SUCESIVO SE LE DENOMINARÁ COMO
    “EL CLIENTE”, AL TENOR DE LAS SIGUIENTES DECLARACIONES Y CLÁUSULAS.
                                 DECLARACIONES.
    I. Declara “ELPRESTADOR”
    a. Ser una sociedad constituida de acuerdo a las leyes de los Estados Unidos Mexicanos, cuyo representante cuenta
       con las facultades suficientes para la celebración del presente contrato, mismas que no le han sido revocadas ni 
       disminuidas hasta el día de hoy.
     
    b. Se encuentra inscrita en el Registro Federal de Contribuyentes.

    c. Que el Sistema de Administración Tributaria (SAT) le ha autorizado las funciones como Proveedor Autorizado de 
       Certificación (PAC) para la emisión de Certificados Fiscales Digitales por Internet (CFDI), hecho comprobable en la
       dirección electrónica http://www.sat.gob.mx/sitio_internet/asistencia_contribuyente/principiantes/comprobantes_fiscales/66_19264.html 
       que obra en el sitio oficial del Sistema de Administración Tributaria.
     
    d. Señala como domicilio legal para todos los efectos de éste contrato el ubicado en calle Avenida Félix Cuevas No.632, 
       primer piso, Delegación Benito Juárez, C.P.03100,en México, Distrito Federal.

    II. Declara “EL CLIENTE” 
    a. Ser quien ha proporcionado de manera veraz y cierta la información requerida por “EL PRESTADOR” en el 
       documento denominado “DATOS SOLICITADOS” que previamente le fue enviado. Asimismo si fuere el caso, que 
       su representante cuenta con las facultades suficientes para la celebración del presente contrato, mismas 
       que no le han sido revocadas ni disminuidas hasta el momento de otorgar el presente consentimiento.
    b. Que se encuentra inscrito en el Registro Federal de Contribuyentes.
     
    c. Que tanto él, como su representante (en los casos que aplique) cuenta con los archivos que contienen 
    su Firma Electrónica Avanzada (de ahora en adelante FIEL), así como sus Certificados de Sello Digital 
    (de ahora en adelante CSD).
    d. Que se le ha mostrado el sistema de facturación electrónica que pone a su disposición NTLink Comunicaciones,
       S.A. de C.V., así como sus calidades, cualidades, usos y potencial comercial. e. Que en cumplimiento de sus 
       obligaciones fiscales, manifiesta su interés y deseo de hacer uso del servicio para la emisión de Certificados 
       Fiscales Digitales a través de Internet de acuerdo a las cláusulas que se describen a continuación.
    f. Que señala como domicilio para todos los efectos legales del presente contrato el domicilio fiscal que aparezca 
       en todos y cada uno de los Certificados Fiscales Digitales que emita a través del sistema que NT LINK pone 
       a su disposición.
     g. Que al marcar y presionar el ícono de “ACEPTACIÓN” implica su voluntad de ingresar sus archivos de FIEL y CSD y la aceptación de los servicios que NT LINK le ha 
    propuesto previamente, bajo las condiciones señaladas en el presente contrato, así como las marcadas en la propuesta enviada previamente y acordada por las 
    partes. III. Declaran Las Partes. a. Que se reconocen mutua y recíprocamente la personalidad con que se ostentan para suscribir el presente contrato. b. Que en 
    el presente contrato no existe la violencia, el dolo o la mala fe. CLÁUSULAS. 
    PRIMERA.- DEFINICIONES. Las Partes convienen que para efectos del presente 
    Contrato los términos que a continuación se señalan, cuando sean empleados con 
    mayúscula la inicial, tendrán la definición que a los mismos se les asigna, sin 
    perjuicio de que sean utilizados en singular o en plural. • Ambiente de 
    Producción. Denominación que se otorga al hecho de someter una aplicación 
    informática o programa de cómputo a situaciones reales donde genera los 
    resultados para los cuales ha sido creado directamente por su usuario final. • 
    Adenda. Servicio adicional e independiente del Sistema de Facturación 
    Electrónica, consistente en información adicional de carácter no fiscal que 
    solicita un receptor al CLIENTE en carácter de emisor, cuyo es uso es 
    obligatorio entre aquellas partes, y que el CLIENTE solicita a NT LINK le 
    programe para el uso del sistema de facturación electrónica de acuerdo a sus 
    necesidades. • Convertidor o Integrador: Servicio adicional e independiente del 
    Sistema de Facturación Electrónica consistente en una aplicación de software que 
    permite el procesamiento, traducción, complementación y conversión de un archivo 
    de un tipo determinado y diferente a un .XML, a un archivo XML, que será 
    sometido al proceso de timbrado ante la autoridad fiscal por parte de NT LINK. • 
    Portal: Dirección electrónica a la que se ingresa a través de la página 
    electrónica www.ntlink.com.mx, opción “Sistema de Facturación”, por medio del 
    cual se puede tener acceso al sistema de NT LINK desde cualquier Terminal a 
    través de la Internet. • Aplicación Local: Servicio adicional y dependiente del 
    Sistema de Facturación Electrónica consistente en una(s) licencia(s) 
    informática(s) que previo pago, brinda NT LINK a EL CLIENTE para su instalación 
    en una sola Terminal en los términos del contrato que al efecto celebre EL 
    CLIENTE con NT LINK, y que conectada a Internet permitirá el uso del sistema de 
    facturación electrónica de NT LINK. • SAT: Sistema de Administración Tributaria. 
    • FIEL: Firma Electrónica Avanzada. • Certificado de Sello Digital o CSD de 
    forma indistinta: Archivo electrónico en el que el contribuyente ha solicitado a 
    la autoridad fiscal una cantidad determinada de operaciones mismo que debe ser 
    proporcionado al sistema de emisión de facturas de NT LINK. Es un requisito 
    indispensable para el éxito de la emisión de los CFDI de EL CLIENTE. • Llave 
    Privada: Archivo con extensión .key que EL CLIENTE genera en el momento de 
    solicitar del SAT su CSD. Este archivo no puede, ni debe entregarse a persona 
    alguna, incluido el personal de NT LINK y su uso se encuentra restringido al 
    CLIENTE y el personal que él designe en su más estricta confianza. • Contraseña 
    de Llave Privada: Clave generada por EL CLIENTE para autentificar el uso de la 
    llave privada. Esta clave es de uso exclusivo de EL CLIENTE, por lo que no 
    puede, ni debe entregarse a persona alguna, incluido el personal de NT LINK, su 
    uso se encuentra restringido al CLIENTE y al personal que él designe en su más 
    estricta confianza. • Terminales: Son los equipos de cómputo con acceso y 
    conexión a Internet que cumplen con las especificaciones técnicas necesarias 
    para mantener una conexión a la Internet y realizar las operaciones de emisión, 
    recepción y elaboración, mismos que pueden o no estar localizados en el interior 
    del establecimiento comercial del Cliente • Certificado Fiscal Digital por 
    Internet o CFDI3.2 de forma indistinta: Documento electrónico contenido en un 
    archivo XML y representado gráficamente mediante un archivo PDF, que cuenta con 
    un sello digital que permite corroborar el origen e integridad del documento, 
    garantizar que la información contenida quede protegida y que sus claves de 
    origen no puedan ser modificadas. • Timbrado o Proceso de Timbrado: Proceso de 
    integración al sistema de facturación electrónica a través de Internet 
    consistente en el envío del archivo TXT por medio del Convertidor TXT–CFDI 3.2 a 
    NT LINK quien lo complementará y enviará al SAT para su autorización y registro 
    definitivo, retornándolo al emisor correspondiente y en su caso al receptor. • 
    Sistema: Solución integral de facturación electrónica que NT LINK pone a 
    disposición del público en general, en toda la República Mexicana en diferentes 
    modalidades para la emisión de sus CFDI. • Propuesta: Documento elaborado y 
    presentado, sea físicamente o por medio de correo electrónico por NT LINK a EL 
    CLIENTE y que éste último aceptó sea mediante la respuesta de aceptación o la 
    aplicación de su firma, en el que se describen las condiciones de precio y cosa 
    o servicio que sirven de base para la celebración del presente contrato. Dicha 
    propuesta forma parte integrante del presente contrato, y se considerará 
    ratificada por las partes a la aceptación del presente instrumento. • 
    Personalización de la Representación Gráfica. Servicio adicional e independiente 
    del Sistema de Facturación Electrónica proporcionado por NT LINK consistente en 
    programar en el sistema los archivos necesarios para que EL CLIENTE reciba y 
    cuente con una representación gráfica con extensión .PDF de los archivos que 
    contienen el CFDI3.2 de acuerdo a parámetros que el propio CLIENTE indica a NT 
    LINK. Ésta Personalización incluye condiciones de forma, y posición tomando como 
    referencia la representación de una hoja tamaño carta en pantalla de una 
    terminal de cómputo, en las secciones superiores e inferiores, así como la 
    posible inserción de un sello de agua, siempre con el diseño, logo o 
    identificación que proporcione el CLIENTE misma que no podrá exceder de 
    50Kilobytes, medida de tamaño electrónico. • Terminales: Son los equipos de 
    cómputo con acceso y conexión a Internet que cumplen con las especificaciones 
    técnicas necesarias para realizar las operaciones de emisión, recepción y 
    elaboración, mismos que pueden o no estar localizados en el interior del 
    establecimiento comercial del Cliente y en donde será instalado el CONVERTIDOR 
    TXT-CFDI 3.2. • Transacciones: Son las operaciones mediante las cuales EL 
    CLIENTE emite CFDI. • XML: XML son las siglas de Extensible Markup Language, una 
    especificación/lenguaje de programación desarrollada por el W3C(World Wide Web 
    Consortium, abreviado W3C, es un consorcio internacional que produce 
    recomendaciones para la utilización de internet). XML es una versión de SGML, 
    diseñado especialmente para los documentos de la web. • WEB SERVICE: Medio 
    electrónico en el que se desarrolla el procedimiento de timbrado y sello digital 
    al que se someten los archivos enviados por el CLIENTE. SEGUNDA.- OBJETO El 
    presente contrato tiene por objeto la utilización del sistema de emisión de 
    comprobantes fiscales digitales a través de Internet (“EL SISTEMA”) en la 
    República Mexicana o en el extranjero diseñado, creado y generado por NT Link 
    Comunicaciones, S.A. de C.V., el cual ha sido certificado por el SAT. Para 
    utilizar “EL SISTEMA”, “EL CLIENTE” requiere en todo momento de una conexión a 
    Internet, y podrá ejecutarlo a través de El Portal, la Aplicación Local o los 
    Convertidores necesarios de acuedo al sistema administrativo o de gestión que 
    posea EL CLIENTE. TERCERA.- RESTRICCIONES AL USO DE LAS CLAVES DE ACCESO. Para 
    el uso del Sistema, “EL CLIENTE” contará con una cuenta única asignada por NT 
    LINK, a efecto de que “EL CLIENTE” pueda realizar la emisión, almacenamiento y 
    consulta de los certificados digitales que emita a través de cualquiera de las 
    opciones señaladas en el párrafo anterior. El cliente puede realizar 
    Transacciones a través de cualquier acceso vía internet utilizando únicamente la 
    clave de acceso generada por el “SISTEMA”, o aquellas que el propio “CLIENTE” 
    hubiere creado de acuerdo a los perfiles que pone a su disposición “EL SISTEMA”. 
    Para que EL CLIENTE obtenga su cuenta única, es necesario que brinde a NT LINK 
    la información requerida en el documento denominado “DATOS SOLICITADOS” que 
    previamente se le ha hecho llegar y que debe enviar a NT LINK. El “CLIENTE” será 
    el único responsable del uso, manejo y resguardo de sus claves de acceso, así 
    como de la secrecía, confidencialidad y grado de confianza que maneje hacía los 
    miembros de su organización con relación a esas claves y sus alcances. CUARTA.- 
    OBLIGACIONES DE “EL PRESTADOR” NT LINK se obliga a: a) Mantener el servicio de 
    timbrado activo y funcionando durante todo el tiempo que dure el contrato. En el 
    caso que por alguna situación imputable a NT LINK hubiere una caída en el 
    sistema, no funcionase o no emitiese las operaciones normalmente, ésta 
    intermitencia no podrá ser mayor a 24 horas, y deberá notificarse a EL CLIENTE. 
    b) Entregar a EL CLIENTE el CFDI debidamente timbrado en su archivo XML y PDF. 
    c) Emitir la factura por el concepto de pago de operaciones que va a consumir y 
    de cualquier otro servicio o producto que requiera. d) Brindar una validez de 
    18(dieciocho) meses a los Certificados que amparan las operaciones adquiridas y 
    aceptadas en la Propuesta para que el CLIENTE haga uso de ellos dentro de ese 
    plazo, aun en el caso que hubiere fenecido el presente contrato. e) En caso que 
    al finalizar los 18(dieciocho) meses el CLIENTE aun tenga operaciones restantes 
    de las adquiridas previamente, NT LINK se compromete a respetar la validez de la 
    cantidad que resulte de aplicar un 10% de dicha cantidad previa sin costo alguno 
    para el CLIENTE. f) Asegurar el resguardo de manera confidencial y a través de 
    mecanismos electrónicos de encriptación y codificación las claves proporcionadas 
    por EL CLIENTE. g) Asignar al cliente una cuenta de usuario y contraseña única 
    de acceso al Sistema donde se emitirán las facturas electrónicas, para efecto de 
    que EL CLIENTE (en su carácter de EMISOR) lleve a cabo la facturación con sus 
    clientes (“RECEPTORES”). h) Poner a su disposición los medios necesarios para 
    generar nuevas claves de acceso, así como de aquellos controles de seguridad que 
    requieran su participación como Usuario Final. i) Proporcionar los servicios que 
    ofrece EL PORTAL de acuerdo al servicio adquirido por el CLIENTE. j) De acuerdo 
    al servicio adquirido por “EL CLIENTE”, brindará a éste la personalización de 
    sus facturas únicamente en los siguientes campos: logo de EL CLIENTE (si lo 
    hubiere) encabezado, pie de página, orientación del documento vertical u 
    horizontal. Cualquier adición, rediseño, campos adicionales que el “CLIENTE” 
    solicite a “EL PRESTADOR” tendrán un costo adicional que será pactado en su 
    momento por “LAS PARTES” y que será colocado dentro de la Propuesta aceptada 
    previamente por el CLIENTE. k) Respetar las promociones que le hayan sido 
    informadas a “EL CLIENTE” ya sea a través de “EL PORTAL”, por correo 
    electrónico, certificado o convencional, durante el tiempo que las mismas 
    establezcan. QUINTA.- OBLIGACIONES DEL CLIENTE. “EL CLIENTE” se obliga a: a) 
    Estar registrado ante el SAT como contribuyente y contar con su cédula fiscal. 
    b) Contar con su FIEL y CSD que le acompañan. c) Contar previamente con los 
    Certificados Digitales que acrediten la solicitud de sellos electrónicos ante el 
    SAT. Asentar en la hoja de datos solicitados la información cierta y veraz, así 
    como proporcionar la documentación que NT LINK le requiera, ya sea de manera 
    física en el domicilio del “PRESTADOR” o electrónica a través de los medios que 
    éste le indique. d) En cualquier caso, cambiar la contraseña proporcionada por 
    “EL PRESTADOR” en la cuenta de usuario única proporcionada en el primer acceso 
    que haga a “EL PORTAL”. e) En cualquier caso, contar con una cuenta de correo 
    electrónico vigente y válida. f) Responder por el uso de la cuenta y contraseña 
    única proporcionada por “EL PRESTADOR” para llevar a cabo la emisión de CFDI. 
    “EL CLIENTE” entiende que es su obligación no transferir, prestar, proporcionar 
    o entregar, bajo ninguna circunstancia, a cualquier tercero la cuenta y 
    contraseña única proporcionada, así como aquella contraseña que el propio 
    “CLIENTE” formule. En virtud de lo anterior libera a “EL PRESTADOR” sus 
    representantes, empleados, colaboradores y directivos de cualquier mal uso que 
    se haga de sus contraseñas y cuenta si por cualquier circunstancia o 
    eventualidad pierde, comparte o le es sustraída esta información aun sin su 
    conocimiento ni consentimiento. g) Cubrir la cuota de inscripción que 
    previamente el “PRESTADOR” previamente le ha hecho de su conocimiento. h) 
    Notificar por escrito a NT Link Comunicaciones, S.A. de C.V. dentro de los cinco 
    días hábiles siguientes a que se efectúe, cualquier cambio de domicilio fiscal 
    al señalado en el documento denominado Datos Solicitados. La falta de 
    notificación de cambio de domicilio por parte de “EL CLIENTE” dará lugar a la 
    rescisión del contrato, así como la cancelación y baja de la cuenta de usuario 
    de acceso al portal, sin responsabilidad alguna para “EL PRESTADOR”. El CLIENTE 
    acepta desde éste momento que será a su cargo cualquier responsabilidad civil, 
    administrativa, fiscal o de cualquier índole que se desprenda de la falta de 
    notificación y actualización del domicilio fiscal; por lo que desde éste momento 
    acepta sacar en paz y a salvo a NT LINK, sus socios, representantes, 
    accionistas, distribuidores y empleados de cualquier controversia que suscite y 
    que ponga en riesgo a NT LINK. i) Cubrir las aplicaciones, requerimientos y 
    adendas que desea previamente le sean programadas en su aplicación, y cuyo costo 
    aceptó previamente en la Propuesta que al efecto se le hizo llegar. j) Cubrir la 
    cantidad de Certificados que previamente solicitó y cuyo costo aceptó, e 
    informar a “EL PRESTADOR” de dicho pago a la dirección electrónica 
    soporte@ntlink.com.mx a fin de asignar la cantidad de certificados requerida. k) 
    Las demás que se señalen en las cláusulas del presente contrato. SEXTA. VALOR DE 
    SERVICIO A CLIENTES. “EL CLIENTE” y “EL PRESTADOR” convienen en que el precio 
    pactado por el servicio de Emisión de Certificados Fiscales Digitales será 
    establecido por “EL PRESTADOR” y aceptado sin condiciones en contra por “EL 
    CLIENTE”. Dicho precio será el que aparece como costo de emisión en la Propuesta 
    que previamente se hizo llegar al cliente. Asimismo, dicho valor podrá ser 
    modificado por NT Link Comunicaciones, S.A. de C.V. haciéndolo del conocimiento 
    de “EL CLIENTE” de forma escrita por medio de correo convencional y/o correo 
    electrónico y/o avisos a través de “EL PORTAL” con por lo menos 15(quince) días 
    de anticipación a su aplicación. SÉPTIMA.- EMISIÓN DE CERTIFICADOS DIGITALES 
    CFDI. “EL CLIENTE” deberá contar con al menos una terminal donde realizará las 
    operaciones necesarias para llevar a cabo el objeto del presente contrato. “EL 
    CLIENTE” solicitará a “EL PRESTADOR” la emisión de Certificados Digitales CFDI 
    de acuerdo a la demanda que requiera conforme a las condiciones comerciales que 
    hayan pactado previamente. “EL CLIENTE” para efecto de llevar a cabo la emisión 
    de Certificados Digitales CFDI, deberá llevar a cabo el procedimiento que a 
    continuación se enuncia. Las PARTES aceptan que el procedimiento que está 
    señalando en la presente clausula es general para el uso del sistema a través 
    del PORTAL, aclarando que el uso del sistema a través de medios tales como 
    Convertidores, o Aplicaciones Locales se regirán por el acuerdo al que lleguen 
    las partes y solo será aplicable todo aquello que no esté contemplado en el 
    contrato respectivo al producto que se trate: I. Previamente “EL CLIENTE” debe 
    ingresar dentro del sistema su FIEL y el Certificado proporcionado por el SAT 
    para la generación de Certificados Digitales. II. “EL CLIENTE” ingresará a 
    través de la terminal en el portal www.ntlink.com.mx. Una vez que ingresó deberá 
    digitar el nombre de usuario (Cuenta de Usuario Única) y Contraseña Única. 
    Cuando haya ingresado deberá de dar de alta al personal autorizado y a todos los 
    contribuyentes a quienes vaya a emitir Certificados Fiscales Digitales CFDI, en 
    las diferentes opciones y módulos dentro del sistema. III. Para la emisión de 
    Comprobantes Fiscales Digitales CFDI, seleccionará del módulo “Facturación” la 
    opción “Emisión de Facturas”, en donde deberá llenar la información que se le 
    solicita. IV. La elaboración del Certificado Fiscal Digital CFDI se considerará 
    exitosa siempre que el comprobante generado bajo la extensión .XML haya sido 
    verificado dentro del portal del SAT, cuya liga se encontrará siempre en el 
    sistema. Es obligación de “EL CLIENTE”, si no cuenta con el servicio de envío, 
    enviar a los contribuyentes por medio de correo electrónico tanto la 
    representación impresa como el archivo con extensión .XML, además de entregar la 
    impresión en papel de la representación impresa si el contribuyente así se lo 
    requiere a “EL CLIENTE”. “EL CLIENTE” es responsable de seguir las indicaciones 
    que se encuentran en el documento electrónico denominado Manual de Usuario, el 
    cual se encuentra en “EL PORTAL”. La falta de aplicación a cualquiera de los 
    requisitos establecidos en el presente contrato responsabiliza únicamente a “EL 
    CLIENTE” de cualquier transacción que pudiera resultar errónea, por lo que NT 
    LINK no será responsable de daños y perjuicios que se ocasionen por el mal uso 
    que derive de la falta de atención y los errores del CLIENTE. OCTAVA. FORMA DE 
    PAGO. LAS PARTES acuerdan que “EL PRESTADOR” cobrará a “EL CLIENTE” la cantidad 
    acordada y aceptada en la Propuesta que previamente le fue enviada. La cantidad 
    de certificados digitales que el “CLIENTE” elija contratar será la que aparezca 
    en la Propuesta que le extienda NT LINK, cuya aceptación deberá comunicarla a NT 
    LINK a través de correo electrónico y/o telefónicamente a través de su agente de 
    ventas o distribuidor autorizado. Salvo pacto en contrario de las partes, EL 
    CLIENTE hará pago previo las operaciones que desee efectuar. En el caso de 
    requerir una cantidad mayor de operaciones, el cliente podrá adquirirlas a 
    través de su Agente de Ventas o distribuidor autorizado haciéndolo de su 
    conocimiento por medio de correó electrónico. Una vez cubierta la cantidad de 
    operaciones en la cuenta bancaria que el “PRESTADOR” pone a su disposición, EL 
    CLIENTE entregará a EL PRESTADOR copia de la ficha de depósito ya sea 
    personalmente o a través de las direcciones o medios electrónicos que “EL 
    PRESTADOR” ponga a su alcance. “EL CLIENTE” se compromete en realizar la 
    adquisición de cualquier cantidad de operaciones de Certificación o Timbrado 
    dentro de los 540 días posteriores a su último pago. En caso contrario, “EL 
    CLIENTE” será notificado por el “PRESTADOR” a través de correo electrónico o por 
    medio del “PORTAL” que su servicio, claves de acceso y contraseña serán 
    suspendidos en el sistema dentro de los quince días posteriores a dicha 
    notificación, con lo que perderá sus derechos de uso del sistema, así como de 
    cualquier promoción con la que hubiera contratado inicialmente. Cualquier pacto 
    en contrario o acuerdo que plasme condiciones diferentes a las aquí planteadas 
    será válido siempre y cuando así lo acuerden las PARTES, mediante la firma 
    expresa del documento en que plasmen sus acuerdos. NOVENA.- RECLAMACIONES 
    Cualquier reclamación que realice algún “RECEPTOR” de Certificados Fiscales 
    Digitales a través de Internet deberá ser atendida por “EL CLIENTE” salvo en los 
    casos que se demuestre que se originó por un problema de sistema. Cualquier 
    reclamación que realice el CLIENTE deberá iniciarla mediante la notificación por 
    escrito sea mediante correo electrónico o convencional dentro de los siguientes 
    15(quince) días hábiles de ocurridos los hechos motivos de reclamación. En éste 
    escrito, el CLIENTE deberá plasmar los hechos motivo de la reclamación, la 
    afectación que se hubiera formulado y sus pretensiones de solución de la 
    reclamación. Hecho lo anterior, NT LINK cuenta con 15(quince) días hábiles para 
    brindar una respuesta al CLIENTE previa investigación de los hechos que de 
    acuerdo al escrito hayan dado pie a la reclamación. Ambas partes están de 
    acuerdo en que “EL PRESTADOR” se deslinda de cualquier responsabilidad o 
    problema que se suscite o llegara a suscitarse respecto de las fallas que 
    pudiesen presentar los sistemas debido a problemas en las comunicaciones 
    (internet), fallas de energía eléctrica, desastres naturales o causas de fuerza 
    mayor, falta de información proporcionada por el CLIENTE, requerimiento 
    extraordinarios formulados por el CLIENTE, falta de seguimiento o de información 
    del receptor hacia el CLIENTE; así como saturación o caída de los sistemas 
    competentes del SAT. DÉCIMA.- DURACIÓN DEL CONTRATO El presente contrato tendrá 
    una duración indefinida a partir de la fecha de su aceptación. Las partes 
    conviene en que cualquiera de ellas podrá dar por terminado el presente contrato 
    sin necesidad de declaración judicial y sin responsabilidad alguna, con la 
    simple notificación por escrito a la otra parte con 30 días de anticipación. No 
    obstante lo anterior, las obligaciones a cargo de ambas Partes que estén 
    pendientes seguirán vigentes hasta su total cumplimiento. Si por cualquier causa 
    de las previstas en el presente contrato que se diera la terminación o rescisión 
    del presente contrato y existiera saldo a favor de “EL PRESTADOR” por concepto 
    de crédito otorgado a “EL CLIENTE”, éste se obliga a pagar a NT Link 
    Comunicaciones, S.A. de C.V. dentro del plazo de cinco días hábiles contados a 
    partir de la notificación correspondiente. DÉCIMA PRIMERA.- TERMINACIÓN 
    ANTICIPADA Son causas de terminación anticipada del presente contrato sin 
    responsabilidad para alguna de las partes: a) El caso fortuito o fuerza mayor, 
    lo cual deberá ser notificado fehacientemente por la parte que lo sufre a la 
    otra. b) Si por alguna causa “EL PRESTADOR” y el SAT dan por terminada la 
    relación entre ellos o existan modificaciones a dicha relación que impidan a NT 
    Link Comunicaciones, S.A. de C.V. seguir prestando los servicios conforme al 
    objeto y términos del presente contrato. c) El vencimiento del plazo forzoso al 
    que está sujeto “EL CLIENTE” que haga uso de los servicios “POS PAGO” Si se 
    diera la terminación del presente contrato en virtud de la causal señalada en el 
    inciso b) y existiera un saldo a favor de “EL CLIENTE” al cual ya no pueda 
    acceder, se procederá de la siguiente forma: 1.- NT Link Comunicaciones, S.A. de 
    C.V. dará aviso a “EL CLIENTE” a través del correo electrónico proporcionado 
    para tal efecto, en un término de 72 horas posterior a la terminación de la 
    relación contractual con el SAT. 2.- “EL CLIENTE” solicitará a NT Link 
    Comunicaciones, S.A. de C.V. por escrito la devolución del remanente del saldo 
    no que pudiera existir dentro del plazo de 30 días naturales, posteriores a la 
    notificación a que refiere el punto anterior. 3.- Recibida la solicitud, NT Link 
    Comunicaciones, S.A. de C.V. contará con un plazo de 30 días naturales para dar 
    contestación a la procedencia de la misma. 4.- Cumplido el plazo y de resultar 
    procedente la devolución del remanente, lo cual será notificado a “EL CLIENTE” 
    por escrito o por correo electrónico, “EL PRESTADOR” contará con 30 días para 
    hacer la devolución del remanente a favor de “EL CLIENTE” previo recibo que al 
    efecto emita. DÉCIMA SEGUNDA.- NO EXCLUSIVIDAD La Partes convienen que el 
    presente Contrato no determina exclusividad alguna en beneficio de “EL CLIENTE” 
    para la emisión de certificados digitales, por lo que NT Link Comunicaciones, 
    S.A. de C.V. queda en completa libertad de contratar adicionalmente con otros 
    clientes, agentes o vendedores dentro de territorio nacional. DECIMA TERCERA.- 
    PENALIZACION En caso de que “EL CLIENTE”, en cualquiera de las modalidades de 
    servicio de las que haga uso, incumpla cualquiera de las obligaciones a su 
    cargo, conforme al presente contrato, se hará acreedor y desde éste momento 
    acepta pagar las penalizaciones que “EL PRESTADOR” le imponga y que consistirán 
    en la cantidad que resulte de multiplicar por doce la inscripción al sistema de 
    “EL PRESTADOR”, atendiendo a las políticas por parte del SAT de acuerdo al 
    contrato original celebrado con este último. Lo anterior sin prejuicio del 
    derecho de NT Link Comunicaciones, S.A. de C.V. de rescindir el presente 
    Contrato, así como proceder a la cancelación y baja de la cuenta única de acceso 
    al portal, sin responsabilidad alguna y sin necesidad de intervención o 
    declaración judicial. No obstante lo anterior, las obligaciones a cargo de “EL 
    CLIENTE” que estén pendientes seguirán vigentes hasta su total cumplimiento. 
    DÉCIMA CUARTA.- MODIFICACIONES “EL CLIENTE” acepta que el presente contrato 
    podrá ser modificado en cualquier tiempo por NT Link Comunicaciones, S.A. de 
    C.V., sin responsabilidad alguna, sea por determinación de NT Link 
    Comunicaciones, S.A. de C.V., o porque el SAT modifique las políticas del 
    contrato original celebrado con NT LINK Comunicaciones, S.A. de C.V., en virtud 
    de lo cual se afecte, altere o modifique todas o alguna de las cláusulas del 
    presente contrato. Ambas partes de común acuerdo aceptan que las modificaciones 
    que se hagan al presente contrato de manera verbal carecerán de todo valor, en 
    el entendido de que las mismas tendrán validez únicamente si se hacen mediante 
    convenio por escrito y/o a través del portal previa aceptación en el mismo por 
    “EL CLIENTE”. Se entenderá ratificada la aceptación a la modificación si “EL 
    CLIENTE” no manifiesta inconformidad alguna pasados 5(cinco) días hábiles a 
    partir de la fecha en que se publico o notificó ya sea a través del portal, a 
    través del correo electrónico o por escrito. En caso de que “EL CLIENTE” no 
    aceptara las modificaciones implementadas por NT Link Comunicaciones, S.A. de 
    C.V. al contrato, sea por escrito y/o a través del portal se tendrá por 
    rescindido el presente contrato sin responsabilidad para alguna de las partes, 
    para lo cual “EL CLIENTE” dará aviso por escrito a NT Link Comunicaciones, S.A. 
    de C.V. de la negativa dentro de los cinco días hábiles posteriores a la 
    notificación, o a su conocimiento a través del portal. Ambas partes están de 
    acuerdo en que NT Link Comunicaciones, S.A. de C.V., podrá migrar o cambiar de 
    dominio en cualquier tiempo el portal sin responsabilidad alguna de lo cual se 
    hará del conocimiento por escrito y/o correo electrónico y/o a través del portal 
    a “EL CLIENTE” con un término de diez días de anticipación, sin que para tal 
    efecto se consideren modificadas la obligaciones y derechos contenidas en las 
    cláusulas del presente Contrato. DÉCIMA QUINTA.- PROPIEDAD DE LOS SISTEMAS 
    Derivado de que “EL CLIENTE” para fines del presente contrato utilizara los 
    sistemas de computo desarrollados por NT Link Comunicaciones, S.A. de C.V., “El 
    CLIENTE” se obliga a utilizarlos únicamente para los fines establecidos en el 
    presente Contrato, por lo que no podrá hacer modificaciones totales o parciales 
    de ningún tipo y bajo ninguna circunstancia a los sistemas, programas o 
    aplicaciones que “EL PRESTADOR” ha puesto a su disposición, por lo que cualquier 
    contravención al respecto hará que “EL CLIENTE” responda de los daños y 
    perjuicios que con ello ocasione a “EL PRESTADOR” asumiendo además toda 
    responsabilidad derivada por violaciones que se causen en materia fiscal, de 
    propiedad industrial o de derechos de autor relacionadas con dichos sistemas, en 
    tal virtud, “EL CLIENTE” se obliga ilimitadamente a sacar en paz y a salvo, así 
    como indemnizar a NT Link Comunicaciones, S.A. de C.V. de cualquier reclamación 
    que se presentare en contra de este último por la violación de derechos que 
    pongan en riesgo las autorizaciones que el SAT ha otorgado a “EL PRESTADOR”, así 
    como a los derechos de propiedad industrial o de derechos de autor 
    pertenecientes a “EL PRESTADOR” o a terceros, cuando dicha reclamación se 
    encuentre relacionada con el desarrollo del objeto de este contrato y/o los 
    sistemas de NT Link Comunicaciones, S.A. de C.V. DÉCIMA SEXTA.- CONFIDENCIALIDAD 
    Amabas partes se obligan a adoptar todos los medios y sistemas disponibles y 
    suficientes para preservar tanto la confidencialidad como el uso restringido de 
    la información confidencial, en los términos de la legislación aplicable. La 
    información confidencial que las partes se proporcionen, únicamente podrá ser 
    utilizada para los fines especificados en este documento, por lo que no podrán, 
    directa o indirectamente, ni a través de persona alguna y en ninguna forma, 
    proporcionar, transferir, publicar, reproducir, o hacer del conocimiento de 
    terceras personas, en ningún tiempo la información confidencial; en caso 
    contrario, la parte que incumpla con esta obligación estará sujeta a las 
    sanciones que la Legislación Mexicana prevé, así como a pagar los daños y 
    perjuicios que ocasione, reservándose la parte agraviada, en todo momento, la 
    facultad de rescindir el presente Contrato. Las Partes podrán proporcionar la 
    Información Confidencial únicamente a su propio personal, y siempre que éste 
    tenga la necesidad de conocer dicha información, para proceder a realizar los 
    fines específicos en el presente Contrato; por tal motivo cada parte dará 
    instrucciones a su propio personal, en relación con la confidencialidad que 
    deben guardar respecto de la Información Confidencial y sobre las penalidades a 
    las cuales estarán sujetos en caso de incumplimiento. Ninguna de las partes 
    incurrirá en responsabilidad alguna, cuando la Información Confidencial que se 
    le haya proporcionado, sea conocida por cualquier tercera Persona, por alguna de 
    las siguientes causas: a) Que la Información Confidencial sea del dominio 
    público; b) Cuando la Información Confidencial se haga del dominio público 
    durante la vigencia del presente Contrato; c) Cuando el titular de la 
    Información Confidencial autorice por escrito a través de sus representantes 
    debidamente autorizada(s) para tal efecto, que la otra Parte difunda la 
    Información Confidencial sin restricciones a terceras Personas, según lo 
    establezca; o d) En caso de que por causa de un procedimiento administrativo o 
    judicial o por algún otro acto de autoridad se requiera a alguna de las Partes 
    que entregue la Información Confidencial que han recibido de la otra Parte a la 
    autoridad que corresponda, lo podrá hacer observando el siguiente procedimiento: 
    (i) La Parte que recibió el requerimiento deberá notificar a la otra dentro de 
    las 24(veinticuatro) horas siguientes a la recepción del requerimiento (ii) La 
    Parte interesada revisará dicho requerimiento y ejercitará ante las autoridades 
    correspondientes todos los medios legales que resulten necesarios o convenientes 
    a fin de evitar la entrega de Información Confidencial, y (iii) En el supuesto 
    de que la entrega de la Información Confidencial sean inminente, la Parte que 
    tenga que entregarla se obliga a garantizar a la otra Parte que en todo momento 
    y en la medida que la legislación aplicable lo permita vigilará que la autoridad 
    a la que se le entregue dicha información Confidencial adopte a las medidas 
    necesarias y suficientes para impedir: (a) que la Información Confidencial 
    pierda tal carácter mediante su divulgación o accesibilidad a terceros; y (b) 
    que dicha autoridad utilice la Información Confidencial de que se trate para un 
    fin distinto al que hubiese señalado en el requerimiento respectivo. Cada una de 
    las Partes reconoce y acepta que la Información Confidencial que haya recibido 
    por cualquier medio o forma y en cualquier momento, así como aquella que en lo 
    futuro reciba conforme al presente Contrato, es y continuará siendo propiedad 
    exclusiva de la Parte que la emita. Cada una de las Partes se obliga a destruir 
    o devolver la Información Confidencial en un término de 5(cinco) días naturales 
    contados a partir de la fecha en que la otra parte se lo solicite. La devolución 
    o destrucción de la Información Confidencial incluye la destrucción o devolución 
    de todos aquellos documentos, muestras y soportes materiales que la contengan, 
    en el entendido de que la Parte que la devuelva se abstendrá en todo caso de 
    retener copias y registros que total o parcialmente contengan Información 
    Confidencial. Las Partes se obligan a hacer entrega de una constancia por 
    escrito de que la Información Confidencial ha sido destruida o devuelta. Por lo 
    anterior, ninguna de las Partes podrá retener o conservar indebidamente la 
    Información Confidencial que le haya sido proporcionada conforme al presente 
    Contrato y no podrá divulgar la Información Confidencial, por un periodo de 2 
    (dos) años, a partir de la fecha de terminación de vigencia del presente 
    Contrato. DÉCIMA SÉPTIMA.- MARCAS PROPIEDAD DE LAS PARTES Cada una de las partes 
    se obliga a respetar las marcas registradas, nombres comerciales, avisos 
    comerciales y cualquier otro signo distintivo (en lo sucesivo “Signos 
    Distintivos”) cuya titularidad o facultad de uso le corresponda a la otra parte 
    o a un tercero. Las partes se abstendrán de utilizar los Signos Distintivos de 
    la otra parte o tercero en aspectos que no sean publicitarios y que no estén 
    estrictamente relacionados con el objeto del presente contrato, para lo cual la 
    parte interesada en utilizar dichos Signos Distintivos de la otra o del tercero 
    deberá contar con la autorización previa y por escrito de quien a su derecho 
    represente. DÉCIMA OCTAVA.- PRIVACIDAD DE LA INFORMACIÓN PERSONAL. En términos 
    de la Ley Federal de Protección de Datos Personales en Posesión de los 
    Particulares, NT LINK COMUNICACIONES, S.A. DE C.V. se obliga a no compartir, 
    distribuir, comercializar, o dar a conocer de forma alguna la información 
    personal de las personas físicas que formen parte del presente contrato, entre 
    la que de forma enunciativa y no limitativa se encuentra el nombre, domicilio, 
    teléfono, RFC. La información proporcionada será utilizada con objeto de dar a 
    conocer a “EL CLIENTE” información relacionada a los servicios de facturación 
    electrónica que presta NT LINK COMUNICACIONES, S.A. DE C.V., así como de 
    aspectos fiscales, legales o contables de interés general. DÉCIMA NOVENA.- 
    RESPONSABILIDAD LABORAL Las “PARTES” contratantes del presente instrumento son 
    totalmente independientes, por lo tanto, no existe ningún nexo o relación 
    obrero-patronal entre sí, quedando entendido que las partes serán las únicas 
    responsables del pago de salarios, prestaciones de Ley, impuestos, derechos y 
    obligaciones que se causen con motivo del personal que contraten el cumplimiento 
    de sus obligaciones, así como de la afiliación de dicho personal y pago de las 
    cuotas correspondientes ente le Instituto Mexicano del Seguro Social (IMSS), 
    Sistema de Ahorro para el Retiro (SAR), el Instituto del Fondo Nacional para la 
    Vivienda de los Trabajadores (INFONAVIT) y la Secretaría de Hacienda y Crédito 
    Público (SHCP). En virtud de lo anterior, las partes se obligan recíprocamente a 
    sacar en paz y a salvo a la otra de cualquier juicio o reclamación que se 
    intente a su contra derivado de los conceptos anteriores por empleados de la 
    otra Parte, y a reembolsarle los gastos que ésta erogue por los conceptos 
    señalados, inmediatamente y contra la entrega de los comprobantes respectivos. 
    VIGÉSIMA.- LÍMITE DE RESPONSABILIDAD. “EL CLIENTE” acepta que NT Link 
    Comunicaciones, S.A. de C.V., no será responsable en caso de que “El CLIENTE” no 
    pueda efectuar sus operaciones, o seguir sus instrucciones debido a: a) 
    desperfectos, suspensión del servicio de los equipos y/o sistemas automatizados 
    del “CLIENTE”; b) fallas en los sistemas de comunicación originados por caso 
    fortuito o fuerza mayor, c) suspensión o fallas de los servicios en los que 
    interviene el SAT; d) falta de información de un receptor; e) falta de 
    aceptación de un receptor al certificado debidamente validado por la autoridad; 
    f) o bien en fallos responsabilidad de “EL CLIENTE”. VIGÉSIMA PRIMERA.- 
    ENCABEZADOS Y JURISDICCIÓN Los encabezados contenidos en cada cláusula del 
    presente contrato carecerán de carácter obligatorio y son utilizados simplemente 
    para una fácil referencia, ya que es únicamente el texto expreso de cada 
    cláusula el que deberá considerarse para efecto de determinar los derechos y 
    obligaciones que las partes adquieren por virtud del presente contrato. En lo no 
    previsto por el presente contrato, las partes acuerdan que serán aplicables las 
    leyes de los Estados Unidos Mexicanos. Para resolver cualquier controversia que 
    se suscite con motivo de la interpretación, cumplimiento o ejecución del 
    presente contrato, las partes se someten expresamente a la jurisdicción de los 
    Tribunales de la Ciudad de México, Distrito Federal, renunciando a cualquier 
    otro fuero que pudiera corresponderles en razón de sus domicilios presentes o 
    futuros.
<br />
</asp:Panel>
</div>
</center>
		<br />
        <asp:Label ID="lblMensajePas" runat="server" Visible="False"></asp:Label>
        <br /> 
         
         <asp:UpdatePanel runat="server">
             <ContentTemplate>
                 <asp:CheckBox ID="cb1" runat="server" 
                     Text="Sí, estoy conforme con todos los términos y condiciones." 
                     AutoPostBack="True" oncheckedchanged="cb1_CheckedChanged"/>
                 
         <br />
        <br />
                 <asp:Button runat="server" ID="btnAceptarPassword" Text="Aceptar" Enabled="False" class="btn btn-primary"
            onclick="btnAceptarPassword_Click" ValidationGroup="CambiarPassword" />
	    &nbsp;&nbsp;
        <asp:Button ID="btnCerrarPassword"  class="btn btn-primary" runat="server" Text="Rechazar" />
             </ContentTemplate>

         </asp:UpdatePanel>

  

  
  
		
	</asp:Panel>
	<asp:Button runat="server" ID="btnPasswordDummy" style="display: none;"/>
</asp:Content>
