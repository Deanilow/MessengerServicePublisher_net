﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessengerServicePublisher.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDataGmailSettingsRecupero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string Definition_Prosegur = string.Empty;
            Definition_Prosegur += $"*PROSEGUR*\n";
            Definition_Prosegur += "🚨 Le informa que ha tenido un\n";
            Definition_Prosegur += "evento de alarma en el predio\n";
            Definition_Prosegur += "ubicado en (var3),\n";
            Definition_Prosegur += "que ha sido *desactivado en menos*\n";
            Definition_Prosegur += "*de un minuto por usuario*\n";
            Definition_Prosegur += "*autorizado.*\n";
            Definition_Prosegur += "Este tipo de evento se considera una\n";
            Definition_Prosegur += "*Falsa Alarma* y no ❌ aplica  para\n";
            Definition_Prosegur += "una operativa de respuesta 🚔.\n";
            Definition_Prosegur += "\n";
            Definition_Prosegur += "Para mayor información comunicarse con nuestra Central de Atención al Cliente *(01)5138686* o \n";
            Definition_Prosegur += "*WSP Aquí:* https://wa.link/nzs8br.\n";
            Definition_Prosegur += "\n";
            Definition_Prosegur += "📌 Ten en cuenta que toda comunicación de nuestra *Central de Monitoreo* será a través del *531-*\n";
            Definition_Prosegur += "*3150*, es importante que guardes el número en tus contactos.\n";
            Definition_Prosegur += "\n";
            Definition_Prosegur += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_Prosegur += "*_informativos._*\n";
            Definition_Prosegur += "\n";
            Definition_Prosegur += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Prosegur", Definition_Prosegur, 1, DateTime.Now, "Created", null, null, null, null });

            string Definition_Contacto = string.Empty;

            Definition_Contacto += "*PROSEGUR*\n";
            Definition_Contacto += "🚨 Le informa que ha tenido *un evento de alarma* en su predio ubicado en (var2), por lo \n";
            Definition_Contacto += "que hemos intentado comunicarnos con los miembros de su lista de contactos de emergencia *SIN*\n";
            Definition_Contacto += "*ÉXITO*.\n";
            Definition_Contacto += "\n";
            Definition_Contacto += "Asimismo, hemos aplicado el protocolo correspondiente ✅ al evento de alarma recibido. Para \n";
            Definition_Contacto += "mayor información comunicarse con nuestra Central de Atención al Cliente *(01)5138686* o WSP \n";
            Definition_Contacto += "*Aquí:* https://wa.link/nzs8br.\n";
            Definition_Contacto += "\n";
            Definition_Contacto += "📌 Ten en cuenta que toda comunicación de nuestra *Central de Monitoreo* será a través del *531-*\n";
            Definition_Contacto += "*3150*, es importante que guardes el número en tus contactos.\n";
            Definition_Contacto += " \n";
            Definition_Contacto += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_Contacto += "*_informativos._*\n";
            Definition_Contacto += "_del negocio.";
            Definition_Contacto += "\n";
            Definition_Contacto += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Contacto", Definition_Contacto, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Acuda = string.Empty;

            Definition_Acuda += "*PROSEGUR*\n";
            Definition_Acuda += "🚨 Tarea asignada\n";
            Definition_Acuda += "para el abonado, (var2)\n";
            Definition_Acuda += "ubicado en, (var3),\n";
            Definition_Acuda += "en el distrito, (var4)\n";
            Definition_Acuda += "📌 Ten en cuenta que de encontrar alguna novedad\n";
            Definition_Acuda += "comunícate con la central para iniciar la operativa.\n";
            Definition_Acuda += "\n";
            Definition_Acuda += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Acuda", Definition_Acuda, 1, DateTime.Now, "Created", null, null, null, null });

            string Definition_Armado = string.Empty;

            Definition_Armado += "||📌🚔. ❌ (var2)\n||";
            Definition_Armado += "||(var3)\n||";
            Definition_Armado += "||(var4)\n||";
            Definition_Armado += "||(var5)\n||";
            Definition_Armado += "_Por favor no responder a este_\n";
            Definition_Armado += "_número ya que está destinado_\n";
            Definition_Armado += "_únicamente para fines informativos_\n";
            Definition_Armado += "_del negocio._\n";
            Definition_Armado += "\n";
            Definition_Armado += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Armado", Definition_Armado, 2, DateTime.Now, "Created", null, null, null, null });



            string Definition_Alerta = string.Empty;

            Definition_Alerta += "||(var1)\n||";
            Definition_Alerta += "||CLIENTE: (var2)\n||";
            Definition_Alerta += "||DIRECCIÓN: (var3)\n||";
            Definition_Alerta += "||COORDENADAS: https://maps.google.com/?q=(var4),(var5)\n||";
            Definition_Alerta += "||FECHA: (var6)\n||";
            Definition_Alerta += "||EVENTO: (var7)\n||";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Alerta", Definition_Alerta, 2, DateTime.Now, "Created", null, null, null, null });


            string Definition_Gps = string.Empty;
            Definition_Gps += "*PROSEGUR*\n";
            Definition_Gps += "🚨 Le informa que ha tenido un\n";
            Definition_Gps += "evento de pánico en la unidad con placa (var2),\n";
            Definition_Gps += "📌 Agradecemos confirmar con el conductor del vehículo,\n";
            Definition_Gps += "si se tratase de una emergencia 🚔 , \n";
            Definition_Gps += "de ser así comuníquese con nuestra central para el operativo\n";
            Definition_Gps += "a través del número *(01) 5138686* o a través\n";
            Definition_Gps += "de nuestro WhatsApp Aquí:\n";
            Definition_Gps += "https://wa.link/nzs8br \n";
            Definition_Gps += "_Por favor no responder a este_\n";
            Definition_Gps += "_número ya que está destinado_\n";
            Definition_Gps += "_únicamente para fines informativos_ \n";
            Definition_Gps += "_del negocio.";
            Definition_Gps += "\n";
            Definition_Gps += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Gps", Definition_Gps, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Call = string.Empty;
            Definition_Call += "*PROSEGUR ALARMS PERU*\n";
            Definition_Call += "Le informa que puede comunicarse con nuestra \n";
            Definition_Call += "Central de Atención al Cliente llamando al\n";
            Definition_Call += "*(01)5138686* o escribiendo a nuestro canal de \n";
            Definition_Call += "WSP *Aquí*: https://wa.link/nzs8br.\n";
            Definition_Call += "*_Por favor no responder a este_\n";
            Definition_Call += "_número ya que está destinado_\n";
            Definition_Call += "_únicamente para fines informativos_*\n";
            Definition_Call += "\n";
            Definition_Call += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Call", Definition_Call, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Fallaac = string.Empty;
            Definition_Fallaac += "*PROSEGUR*\n";
            Definition_Fallaac += "💡 Le informa que se ha presentado una *FALLA DE AC* en el local (var2) \n";
            Definition_Fallaac += "ubicado en (var3). \n";
            Definition_Fallaac += "se sugiere revisar la conexión de energía o contactar con su proveedor.\n";
            Definition_Fallaac += "\n";
            Definition_Fallaac += "Para mayor información comunicarse con nuestra Central de Atención al Cliente.\n";
            Definition_Fallaac += "\n";
            Definition_Fallaac += "📌 Ten en cuenta que toda comunicación de nuestra *Central de Monitoreo* será a través del *531-*\n";
            Definition_Fallaac += "*3150*, es importante que guardes el número en tus contactos.\n";
            Definition_Fallaac += "\n";
            Definition_Fallaac += "*_Por favor no responder a este número ya que está destinado únicamente para fines_* \n";
            Definition_Fallaac += "*_informativos._*\n";
            Definition_Fallaac += "\n";
            Definition_Fallaac += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Fallaac", Definition_Fallaac, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Qido1 = string.Empty;
            Definition_Qido1 += $"Hola! 🐻  \n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"👀 ¿Sabías que instalar tu Alarma Qido \n";
            Definition_Qido1 += $"toma *tan solo 40 a 45 minutos?*\n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"Activa tu Alarma Qido y vive sin \n";
            Definition_Qido1 += $"preocupaciones mientras nosotros \n";
            Definition_Qido1 += $"*cuidamos lo que más quieres* 🧡🏡 \n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"Para instalar tu Alarma Qido tienes 2 \n";
            Definition_Qido1 += $"opciones:\n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"1.*Auto instalar tu Alarma*, al descargar el \n";
            Definition_Qido1 += $"App Qido, un instructivo te guiará paso a \n";
            Definition_Qido1 += $"paso. Tutorial AQUÍ:  \n";
            Definition_Qido1 += $"https://www.youtube.com/watch?v=5vK2gfpKkbI \n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"2.*Soporte Telefónico*, nuestros \n";
            Definition_Qido1 += $"Especialistas de Seguridad Qido pueden  \n";
            Definition_Qido1 += $"guiarte durante el proceso de instalación, \n";
            Definition_Qido1 += $"contáctanos al (01) 513-8607 o escríbenos  \n";
            Definition_Qido1 += $"vía WhatsApp al 980-024 240 de Lunes a  \n";
            Definition_Qido1 += $"*Domingo de 9 AM a 6 PM*\n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"*🟢  ELIJO   ⚠️ INSTALO  🔴 CUIDO *\n";
            Definition_Qido1 += $"\n";
            Definition_Qido1 += $"*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Qido1", Definition_Qido1, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Qido2 = string.Empty;
            Definition_Qido2 += "Hola! 🐻  \n";
            Definition_Qido2 += "\n";
            Definition_Qido2 += "Han pasado (var2) desde que compraste tu Alarma Qido pero vemos que tu vivienda aún sigue \n";
            Definition_Qido2 += "desprotegida 🏠🔓⚠️\n";
            Definition_Qido2 += "\n";
            Definition_Qido2 += "Recuerda que tus dispositivos Qido tienen *1 año de garantía* ⏳, contáctanos al *(01) 513-8607* o \n";
            Definition_Qido2 += "escríbenos vía WhatsApp al *980-024 240* para guiarte durante el proceso de Instalación. Te \n";
            Definition_Qido2 += "ofrecemos un horario extendido de atención de *Lunes a Domingo de 9 AM a 6 PM.* \n";
            Definition_Qido2 += "\n";
            Definition_Qido2 += "Activa tu Alarma Qido y vive sin preocupaciones mientras nosotros cuidamos lo que más quieres 🧡🏠 \n";
            Definition_Qido2 += "\n";
            Definition_Qido2 += "*🟢  ELIJO   ⚠️ INSTALO  🔴 CUIDO *\n";
            Definition_Qido2 += "\n";
            Definition_Qido2 += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Qido2", Definition_Qido2, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Caida = string.Empty;
            Definition_Caida += "*PROSEGUR ALARMS*\n";
            Definition_Caida += "\n";
            Definition_Caida += "Le informa que hemos detectado *INTERMITENCIAS* 🚨  en la comunicación de su sistema de \n";
            Definition_Caida += "alarmas ubicado en (var2)";
            Definition_Caida += "\n";
            Definition_Caida += "Por su seguridad, es necesario que se contacte con nosotros al *(01)5138686* o a través de nuestro \n";
            Definition_Caida += "WhatsApp *Aquí*: https://wa.link/82j5ys\n";
            Definition_Caida += "\n";
            Definition_Caida += "📌 Es importante que previamente revise lo siguiente:\n";
            Definition_Caida += "\n";
            Definition_Caida += "☑️ Que cuente con fluido eléctrico\n";
            Definition_Caida += "☑️ Que su sistema de alarmas esté conectado\n";
            Definition_Caida += "☑️ Si su alarma transmite por línea telefónica validar que la línea este operativa\n";
            Definition_Caida += "\n";
            Definition_Caida += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_Caida += "*_informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Caida", Definition_Caida, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Intento = string.Empty;
            Definition_Intento += "*PROSEGUR ALARMS*\n";
            Definition_Intento += "\n";
            Definition_Intento += "Le informa que hemos detectado *INTERMITENCIAS* 🚨  en la comunicación de su sistema de\n";
            Definition_Intento += "alarmas ubicado en (var2). Hemos intentado contactarnos con todos los miembros de su  \n";
            Definition_Intento += "lista de contactos *SIN ÉXITO*. \n";
            Definition_Intento += "\n";
            Definition_Intento += "Por su seguridad, es necesario que se contacte con nosotros al *(01)5138686* o a través de nuestro  \n";
            Definition_Intento += "WhatsApp *Aquí*: https://wa.link/82j5ys\n";
            Definition_Intento += "\n";
            Definition_Intento += "📌 Es importante que previamente revise lo siguiente:\n";
            Definition_Intento += "\n";
            Definition_Intento += "☑️ Que cuente con fluido eléctrico \n";
            Definition_Intento += "☑️ Que su sistema de alarmas esté conectado\n";
            Definition_Intento += "☑️ Si su alarma transmite por línea telefónica validar que la línea este operativa\n";
            Definition_Intento += "\n";
            Definition_Intento += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_Intento += "*_informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";


            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Intento", Definition_Intento, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Cotizacion = string.Empty;
            Definition_Cotizacion += "*PROSEGUR ALARMS*\n";
            Definition_Cotizacion += "\n";
            Definition_Cotizacion += "Le recuerda que tiene una *COTIZACIÓN PENDIENTE* necesaria para *asegurar la operatividad* de \n";
            Definition_Cotizacion += "su sistema de alarmas, ubicado en (var2).  \n";
            Definition_Cotizacion += "\n";
            Definition_Cotizacion += "📌 Si requiere de alguna ayuda o de mayor información contáctenos al *(01)5138686* o a nuestro \n";
            Definition_Cotizacion += "WhatsApp *Aquí*: https://wa.link/1vesv3\n";
            Definition_Cotizacion += "\n";
            Definition_Cotizacion += "*_Por favor no responder a este número ya que está destinado unicamente para fines_* \n";
            Definition_Cotizacion += "*_informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Cotizacion", Definition_Cotizacion, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_AlarmaPanico = string.Empty;
            Definition_AlarmaPanico += "*PROSEGUR*\n";
            Definition_AlarmaPanico += "\n";
            Definition_AlarmaPanico += "🚨 Le informa que ha tenido *UN EVENTO DE PÁNICO*  en el local:(var2) ubicado en (var3) \n";
            Definition_AlarmaPanico += "\n";
            Definition_AlarmaPanico += "✅  Para mayor información comunicarse con nuestra Central de Atención al Cliente. \n";
            Definition_AlarmaPanico += "\n";
            Definition_AlarmaPanico += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_AlarmaPanico += "*_informativos del negocio._*\n";
            Definition_AlarmaPanico += "\n";
            Definition_AlarmaPanico += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "AlarmaPanico", Definition_AlarmaPanico, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Notificacion1 = string.Empty;
            Definition_Notificacion1 += "*PROSEGUR*\n";
            Definition_Notificacion1 += "\n";
            Definition_Notificacion1 += "🚨 Le informa que ha tenido *UN EVENTO DE ALARMA*  en el local:(var2) ubicado en (var3) \n";
            Definition_Notificacion1 += "\n";
            Definition_Notificacion1 += "✅  Para mayor información comunicarse con nuestra Central de Atención al Cliente. \n";
            Definition_Notificacion1 += "\n";
            Definition_Notificacion1 += "📌 Ten en cuenta que toda comunicación de nuestra Central de Monitoreo será a través del \n";
            Definition_Notificacion1 += "*531-3150*, es importante que guardes el número en tus contactos.\n";
            Definition_Notificacion1 += "\n";
            Definition_Notificacion1 += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_Notificacion1 += "*_informativos._*\n";
            Definition_Notificacion1 += "\n";
            Definition_Notificacion1 += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Notificacion1", Definition_Notificacion1, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Aniego = string.Empty;
            Definition_Aniego += "*PROSEGUR*\n";
            Definition_Aniego += "\n";
            Definition_Aniego += "🚨 Le informa que ha tenido UN EVENTO DE ANIEGO  en el local: (var2) \n";
            Definition_Aniego += "ubicado en (var3)\n";
            Definition_Aniego += "\n";
            Definition_Aniego += "✅  Para mayor información comunicarse con nuestra Central de Atención al Cliente. \n";
            Definition_Aniego += "\n";
            Definition_Aniego += "*_Por favor no responder a este número ya que está destinado únicamente para fines_*\n";
            Definition_Aniego += "*_informativos._*\n";
            Definition_Aniego += "\n";
            Definition_Aniego += "*_PROSEGUR ACTIVA PERÚ S.A. -20517930998_*";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Aniego", Definition_Aniego, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Programacion1 = string.Empty;
            Definition_Programacion1 += "*PROSEGUR ALARMS*\n";
            Definition_Programacion1 += "\n";
            Definition_Programacion1 += "Le informamos que su *VISITA TÉCNICA* ha sido programada 📆 para el día *(var2)* en el rango \n";
            Definition_Programacion1 += "horario de *(var3)*. en su inmueble ubicado en *(var4)*.\n";
            Definition_Programacion1 += "\n";
            Definition_Programacion1 += "👉 Tener en cuenta las siguientes recomendaciones: \n";
            Definition_Programacion1 += "\n";
            Definition_Programacion1 += "☑️ Un responsable deberá recibir al técnico en el rango de atención asignado y poder *autorizar* \n";
            Definition_Programacion1 += "los trabajos que se requieran para dar solución al problema.\n";
            Definition_Programacion1 += "\n";
            Definition_Programacion1 += "☑️ Podrá *reprogramar/cancelar* su visita en el *App SMART* con 24h de anticipación.\n";
            Definition_Programacion1 += "\n";
            Definition_Programacion1 += "📌 Si requiere mayor información contáctenos al *(01)5138686* o a nuestro WhatsApp *Aquí*: wa.link/4usuq1.";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Programacion1", Definition_Programacion1, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Programacion2 = string.Empty;
            Definition_Programacion2 += "Estimado(a), *(var2)*\n";
            Definition_Programacion2 += "\n";
            Definition_Programacion2 += "Hemos intentado contactarlo(a) *sin éxito* 😔 para coordinar la fecha de su visita técnica ya que no  \n";
            Definition_Programacion2 += "ha sido posible programar la atención para el *(var3)*.\n";
            Definition_Programacion2 += "\n";
            Definition_Programacion2 += "Sin embargo; la fecha más próxima disponible 📆 que podemos ofrecer es para el *(var4)* en el  \n";
            Definition_Programacion2 += "rango horario de *(var5)*.\n";
            Definition_Programacion2 += "\n";
            Definition_Programacion2 += "📌 Agradeceremos *CONFIRMAR* la agenda técnica Aquí 👉 https://wa.link/9dgfdw en un *plazo \n";
            Definition_Programacion2 += "máximo de 2 horas* o caso contrario coordinar una *nueva fecha* en el mismo enlace.\n";
            Definition_Programacion2 += "\n";
            Definition_Programacion2 += "Por favor no responder a este número ya que está destinado únicamente para fines \n";
            Definition_Programacion2 += "*_informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Programacion2", Definition_Programacion2, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Programacion3 = string.Empty;
            Definition_Programacion3 += "Estimado(a), *(var2)*\n";
            Definition_Programacion3 += "\n";
            Definition_Programacion3 += "Te saludamos de PROSEGUR, queremos informarte que no ha sido posible programar la atención \n";
            Definition_Programacion3 += "técnica para el *(var3)* 😔.\n";
            Definition_Programacion3 += "\n";
            Definition_Programacion3 += "Sin embargo; la fecha más próxima disponible 📆 que podemos ofrecer es para el *(var4)* en el \n";
            Definition_Programacion3 += "rango horario de *(var5)*.\n";
            Definition_Programacion3 += "\n";
            Definition_Programacion3 += "📌 Agradeceremos *CONFIRMAR* la agenda técnica *Aquí* 👉 https://wa.link/9dgfdw en un *plazo* \n";
            Definition_Programacion3 += "*máximo de 2 horas* o caso contrario coordinar una *nueva fecha* en el mismo enlace.\n";
            Definition_Programacion3 += "\n";
            Definition_Programacion3 += "Por favor no responder a este número ya que está destinado únicamente para fines \n";
            Definition_Programacion3 += "*_informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Programacion3", Definition_Programacion3, 1, DateTime.Now, "Created", null, null, null, null });


            string Definition_Programacion4 = string.Empty;
            Definition_Programacion4 += "Hola,\n";
            Definition_Programacion4 += "\n";
            Definition_Programacion4 += "Hemos intentado contactarlo(a) para darle respuesta a su *solicitud administrativa* ingresada el \n";
            Definition_Programacion4 += "*(var2)*. Por favor, para conocer la resolución del caso contactarnos a nuestro canal wsp Aquí 👉 https://wa.link/8plcem. \n";
            Definition_Programacion4 += "\n";
            Definition_Programacion4 += "Por favor no responder a este número ya que está destinado únicamente para fines \n";
            Definition_Programacion4 += "*_informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Programacion4", Definition_Programacion4, 1, DateTime.Now, "Created", null, null, null, null });

            string Definition_Recupero = string.Empty;
            Definition_Recupero += "Estimado(a), *(var2)*\n";
            Definition_Recupero += "\n";
            Definition_Recupero += "Nos estamos contactando debido a que hoy *(var3)* teníamos agendado el recojo de lo equipos propiedad de Prosegur Activa, y no hemos podido localizarlo en su inmueble. Es necesario para evitar contingencias legales que reagende la visita técnica en un plazo de 48 horas.\n";
            Definition_Recupero += "\n";
            Definition_Recupero += "📌Contáctenos a nuestra central de atención 513-8686 o nuestro canal de Atención WSP Aquí 👉 https://wa.link/nzs8br";
            Definition_Recupero += "\n";
            Definition_Recupero += "*_Por favor no responder a este número ya que está destinado únicamente para fines \n";
            Definition_Recupero += "informativos. PROSEGUR ACTIVA PERU S.A. - 20517930998_*\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "Recupero", Definition_Recupero, 1, DateTime.Now, "Created", null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
