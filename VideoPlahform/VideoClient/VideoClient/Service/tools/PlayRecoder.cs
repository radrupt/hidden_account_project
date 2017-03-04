using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hie_MP4Player;
using System.Runtime.InteropServices;

namespace VideoClient.Service.tools
{
    class PlayRecoder
    {
        private IntPtr hwnd;     //显示窗口句柄
        private IntPtr parenthwnd; //显示窗口所在窗口的句柄
        private int port;        //通道号
        private Boolean is_pause;      //当前是否暂停

        private Boolean is_openfile; // 打开文件后可做解码播放操作
        private Boolean is_play;     //解码播放操作后可作暂停，停止工作
        private string filepathname;
        private Boolean filerefcreated;//表明文件索引是否做好,因为MP4Play_GetKeyFramePos和MP4Play_GetNextKeyFramePos和
                                        //MP4Play_SetCurrentFrameNum和MP4Play_OneByOneBack会用到
                                        //如果为假,就表明这四个函数不可使用
        private HIE_MP4Player.pFileRefDone cb_filerefdone;   //文件索引后回调函数
        HIE_MP4Player.DisplayCBFun cndiaplayfun;             //抓图回调
        private HIE_MP4Player.SourceBufCallBack cb_sourcebuf;  //

        public PlayRecoder(string filepathname, int port, IntPtr hwnd, IntPtr parenthwnd)
        {
            this.port = port;
            this.hwnd = hwnd;
            this.filepathname = filepathname;
            this.parenthwnd = parenthwnd;

            Init();
            OpenFile();

            ////流播放
            //cb_sourcebuf = new HIE_MP4Player.SourceBufCallBack(CB_sourcebuf);
            //HIE_MP4Player.MP4Play_SetSourceBufCallBack(port, 50 * 1024, cb_sourcebuf, 0, (IntPtr)0);
            //HIE_MP4Player.MP4Play_SetStreamOpenMode(port, 0);
            //long fileheadlength = HIE_MP4Player.MP4Play_GetFileHeadLength();
            //byte[] headdata = { 0X00, 0X00, 01, 0XC6, 0X4D, 0X50, 0X47, 0X34, 0x01, 0x00, 0X19, 0X20, 0XA1, 0x07, 0x00, 0X60, 0x01, 0X20, 0x01 };
            //HIE_MP4Player.MP4Play_OpenStream(port, headdata, 19, 2 * 50 * 1024);
        }
        public void Init() 
        {
            is_pause            = false;   //当前未暂停
            is_openfile         = false;
            is_play             = false;
            filerefcreated      = false;
            LockNewPort();//锁通道
            cb_filerefdone = new HIE_MP4Player.pFileRefDone(CB_FileRefDone);//注册回调函数
            Boolean my_player = HIE_MP4Player.MP4Play_SetFileRefCallBack(port, cb_filerefdone, 0);//添加文件索引后的回调函数
            if ( my_player != true ) return;

            HIE_MP4Player.MP4Play_SetFileEndMsg(port, parenthwnd, Hie_MP4Player.HIE_MP4Player.MYMESSAGE);
        }
        
        public void OpenFile() 
        {
            if (!HIE_MP4Player.MP4Play_OpenFile(port, filepathname))
                throw new Exception("文件" + filepathname + ": 打开失败");
            is_openfile = true;
        }
        public void Play()
        {
            if (!is_openfile) return;
            if (!HIE_MP4Player.MP4Play_Play(port, hwnd) || !HIE_MP4Player.MP4Play_PlaySound(port))
                throw new Exception("文件" + filepathname + ": 播放失败");
            is_play = true;

        }
        public void Pause() {
            if (!is_play) return;  //如果没有播放则无法暂停,直接返回
            is_pause = !is_pause;  
            if (!HIE_MP4Player.MP4Play_Pause(port, is_pause))
                if( is_pause )
                   throw new Exception("文件"+filepathname+": 视频暂停失败");     
                else
                   throw new Exception("文件"+filepathname+": 视频取消暂停失败");   
        }   
        public void Stop()//真心感觉这个没多少用
        {
            if (!is_play) return;    //如果没有播放则无需停止,直接返回
            if (!HIE_MP4Player.MP4Play_Stop(port) || !HIE_MP4Player.MP4Play_StopSound())
                throw new Exception("文件"+filepathname+": 停止失败");
            is_play = false;
            is_pause = false;
        }
        //如果文件未打开，那就不可以关闭
        //如果文件打开了,但是没有停止文件解码，得先停止解码
        public void Close()
        {
            if (!is_openfile) return;  //如果没有打开文件,则return
            if (is_play) Stop();       //如果没停止播放,则先停止
            if (!HIE_MP4Player.MP4Play_CloseFile(port) )
                throw new Exception("文件"+filepathname+": 关闭失败");
            is_openfile = false;
            UnLockPort();
        }
        public void LockNewPort()
        {
            if (!HIE_MP4Player.MP4Play_LockNewPort(ref port))
                throw new Exception("锁通道" + port + "失败");
        }
        public void UnLockPort()
        {
            if (!HIE_MP4Player.MP4Play_UnlockPort( port ))
                throw new Exception("解锁锁通道"+port+"失败");
        }

        public void LocalZoom() {
            HIE_MP4Player.MP4Play_LocalZoom(port, true, 50, 50, 320, 320);
        }

        public void SnapBMP() {
            int pWidth = 0;
            int pHeight = 0;
            if( !HIE_MP4Player.MP4Play_GetPictureSize(port,ref pWidth,ref pHeight) )  return;
            MessageBox.Show(pWidth.ToString()+" "+pHeight.ToString());
            int nsize = 54 + pWidth * pHeight * 3;
            Byte[] buff = new Byte[nsize];
            
            int type = 3;
            MessageBox.Show(nsize.ToString() );
            if (!HIE_MP4Player.MP4Play_SnapBMP(port, buff, ref nsize, ref pWidth, ref pHeight, ref type))
                MessageBox.Show("sdfds");
            int size = Marshal.SizeOf(buff[0]) * buff.Length;
            IntPtr destbuff = Marshal.AllocHGlobal(size);;
            Marshal.Copy(buff, 0, destbuff, size);
            MessageBox.Show(nsize.ToString()+" "+size.ToString());
            Boolean savepicture = HIE_MP4Player.MP4Play_ConvertToBmpFile(destbuff, size, pWidth, pHeight, type, "D;\\qq.bmp");
            if (!savepicture) MessageBox.Show("失败");
            //回调方式保存图片未实现
            //cndiaplayfun = new HIE_MP4Player.DisplayCBFun(CB_DisplayFun);
            //HIE_MP4Player.MP4Play_SetDisplayCallBack(port, cndiaplayfun);
        }

        private void CB_DisplayFun(int nPort,
             IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nReserved)
        {
           
            Boolean savepicture = HIE_MP4Player.MP4Play_ConvertToBmpFile(pBuf, nSize, nWidth, nHeight, nType, "D;\\qq.bmp");
            if (!savepicture) MessageBox.Show("失败");
        }
        private void CB_FileRefDone(int nPort, uint nUser)
        {
            filerefcreated = true;
        }
        //public void CB_sourcebuf(int nPort, uint nBufSize, uint dwUser, IntPtr pResvered)
        //{
        //    //HIE_MP4Player.MP4Play_InputData(nPort, nBufSize);
        //}

    }
}
