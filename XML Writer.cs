//XML WRITER

XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
xmlWriterSettings.Indent = true;
xmlWriterSettings.OmitXmlDeclaration = true;
XmlWriter writer = XmlWriter.Create(FormMain.filepath.ToString() + "\\PowerBulkloadPreferences.xml", xmlWriterSettings);


writer.WriteStartElement("settings");

writer.WriteStartElement("bulkloadEXEFile");
writer.WriteString("\"C:\\Program Files (x86)\\Smart3D\\CatalogData\\BulkLoad\\Bin\\Bulkload.exe\"");
writer.WriteEndElement();

writer.WriteStartElement("rootpath");
writer.WriteString(txtSC.Text);
writer.WriteEndElement();

writer.WriteStartElement("server");
writer.WriteString(FormMain.strServer);
writer.WriteEndElement();

writer.WriteStartElement("site");
writer.WriteString(FormMain.strSite);
writer.WriteEndElement();

writer.WriteEndElement();
writer.Close();