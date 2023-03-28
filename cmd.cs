//EXECUTANDO PROCESSOS NO CMD

proc = new Process();
ProcessStartInfo processStartInfo = new ProcessStartInfo();
processStartInfo.FileName = "cmd.exe";
processStartInfo.RedirectStandardInput = true;
processStartInfo.RedirectStandardOutput = true;
processStartInfo.UseShellExecute = false;
processStartInfo.CreateNoWindow = false;
processStartInfo.Arguments = " /C " + FormMain.strBL;
proc.StartInfo = processStartInfo;
proc.EnableRaisingEvents = true;
proc.Start();

//Pausa por 3segundos:
Thread.Sleep(3000);

proc.Close();