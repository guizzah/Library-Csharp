     private void FormMain_Load(object sender, EventArgs e)
        {
            //CARREGANDO O ARQUIVO DE PREFERENCIAS
            sy1 = new Symbols();

            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            filepath = System.IO.Path.GetDirectoryName(strExeFilePath);
            if (System.IO.File.Exists(filepath.ToString() + "\\PowerBulkloadPreferences.xml"))
            {
                XmlReader reader = XmlReader.Create(filepath.ToString() + "\\PowerBulkloadPreferences.xml");
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "bulkloadEXEFile")
                        {
                            XElement el = (XElement)XNode.ReadFrom(reader);
                            bulkloadEXE = el.Value;
                        }

                        if (reader.Name == "rootpath")
                        {
                            XElement el = (XElement)XNode.ReadFrom(reader);
                            sy1.txtSC.Text = el.Value;
                        }

                        if (reader.Name == "server")
                        {
                            XElement el = (XElement)XNode.ReadFrom(reader);
                            txtServer.Text = el.Value;
                        }

                        if (reader.Name == "site")
                        {
                            XElement el = (XElement)XNode.ReadFrom(reader);
                            txtSite.Text = el.Value;
                        }

                    }
                  
                }
                reader.Close();
            }
         }