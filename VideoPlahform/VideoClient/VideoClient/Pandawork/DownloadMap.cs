using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClient.DownLoadServiceReference;

namespace VideoClient.Pandawork
{
    public class DownloadMap
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void downloadMaps()
        {
            log.Debug("开始更新本地地图 downloadMaps（）");
            string mapPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) 
                + "\\" + Properties.Settings.Default.NAME + "\\maps\\";
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }
            if (Directory.Exists(mapPath))
            {
                string[] currentMaps = Directory.GetFiles(mapPath);
                log.Debug("本地的地图有：" + currentMaps.Length + "个");
                List<String> haveNoMaps = new List<string>();

                MapServiceReference.MapServiceClient msc = new MapServiceReference.MapServiceClient();
                string[] serverMaps = msc.getAllMapImageNames();
                log.Debug("服务器有地图" + serverMaps.Length + "个");
                foreach (string map in serverMaps)
                {
                    if (!currentMaps.ToList().Contains(map))
                    {
                        haveNoMaps.Add(map);
                    }
                }
                foreach (string mapName in haveNoMaps)
                {
                    if (mapName != null && mapName.Trim() != "")
                    {
                        log.Debug("本地没有的地图是：" + mapName);
                        download(mapName);
                    }
                }
            }
            else
            {
                log.Debug("无法创建用户地图目录：" + mapPath);
            }
        }

        private static void download(string fileName)
        {
            //TODO 需要判断下是否当前程序还在运行
            bool isExit = false;
            //下载地图文件保存路径
            string saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + "\\" + Properties.Settings.Default.NAME + "\\maps\\"+fileName;
            DownLoadServiceClient dsc = new DownLoadServiceClient();
            //从服务器中获取地图文件流
            Stream sourceStream = dsc.DownLoadFile(fileName);
            if (sourceStream != null)
            {
                if (sourceStream.CanRead)
                {
                    using (FileStream fs = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        const int bufferLength = 4096;
                        byte[] myBuffer = new byte[bufferLength];
                        int count;
                        while ((count = sourceStream.Read(myBuffer, 0, bufferLength)) > 0)
                        {
                            if (isExit == false)
                            {
                                fs.Write(myBuffer, 0, count);
                            }
                            else//窗体已经关闭跳出循环
                            {
                                break;
                            }
                        }
                        fs.Close();
                        sourceStream.Close();
                    }
                }
            }
        }
    }
}
