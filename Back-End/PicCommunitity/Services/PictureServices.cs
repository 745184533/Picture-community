using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Services
{
    public class PictureServices
        
    {
        private AppDbContext context;

        public PictureServices(AppDbContext context)
        {
            this.context = context;
        }

        //获取对应id的User，不存在则返回Null
        public users GetUserById(string userId)
        {
            return context.users.FirstOrDefault(u => u.u_id == userId);
        }

        //获取某个用户对应的点赞信息
        public likespicture getLikes(string userId,string picId)
        {
            return context.likesPicture.FirstOrDefault(l => l.u_id == userId && l.p_id == picId);
        }

        //获取图片
        public picture getPicture(string picId)
        {
            return context.picture.FirstOrDefault(p=>p.p_id==picId);
        }

        //获取图片的总收藏数
        public int getPicStarNum(string picId)
        {
            return context.favoritePicture.Count(f => f.p_id == picId);
        }

        //获取图片总的评论数
        public int getPicCommentNum(string picId)
        {
            return context.picComment.Count(p => p.p_id == picId);
        }

        public string[] getTag(string url)
        {
            string strInput = "python C:/ProgramData/Anaconda3/envs/ML/MobileNet/typeSever.py";
            //string strInput = "python D:/Anaconda3/envs/ML/MobileNet/typeSever.py";
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(strInput + "&exit");
            p.StandardInput.AutoFlush = true;
            //  string strOuput = p.StandardOutput.ReadToEnd();
            p.StandardInput.WriteLine(url + "\n");
            //获取输出信息
            string strOuput = "tset";
            string[] Out = new string[10];
            while (true)
            {
                strOuput = p.StandardOutput.ReadLine();
                if (strOuput == "Tags") break;

            }
            for (int i = 0; i < 10; i++)
                Out[i] = p.StandardOutput.ReadLine();

            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            return Out;

        }


    }
}
