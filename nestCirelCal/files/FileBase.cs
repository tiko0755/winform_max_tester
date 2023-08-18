using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace goldenman.Files
{
    class FileBase
    {
        public FileBase():this("")
        {
            
        }

        public FileBase(string strFilePath)
            : this(strFilePath, 
            strFilePath.LastIndexOf('\\') < 0 ?"" : strFilePath.Substring(0, strFilePath.LastIndexOf('\\')),
            strFilePath.LastIndexOf('\\') < 0 ? "" : strFilePath.Substring(strFilePath.LastIndexOf('\\')+1,
            strFilePath.Length-strFilePath.LastIndexOf('\\')-1))
        { 
            
        }

        public FileBase(string strFilePath,string strFileName):this(strFilePath,strFilePath.LastIndexOf('\\')<0?  
            "":strFilePath.Substring(0, strFilePath.LastIndexOf('\\')),strFileName)
        {
            this.strFilePath = strFilePath;
        }

        public FileBase(string strFilePath, string strDirPath,string strFileName)
        {
            this.strFilePath = strFilePath;
            this.strDirPath = strDirPath;
            this.strFileName = strFileName;
        }

        private string strFileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string StrFileName
        {
            get { return strFileName; }
            set
            {
                strFileName = value;
            }
        }

        private string strFilePath;
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string StrFilePath
        {
            get { return strFilePath; }
            set
            {
                strFilePath = value;
            }
        }

        private string strDirPath;
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string StrDirPath
        {
            get { return strDirPath; }
            set
            {
                strDirPath = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsFileExist()
        {
            return File.Exists(strFilePath);
        }

        /// <summary>
        /// 判断文件夹存在与否
        /// </summary>
        /// <returns></returns>
        public bool IsDirExist()
        {
            return Directory.Exists(strDirPath);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        public void CreateDir()
        {
            Directory.CreateDirectory(strDirPath);
        }

        /// <summary>
        /// 读取一行文件
        /// </summary>
        /// <returns></returns>
        public string ReadLineFile()
        {
            string strResult = "";

            if (IsDirExist() && IsFileExist())
            {
                StreamReader sr = new StreamReader(strFilePath);
                strResult = sr.ReadLine();
                sr.Close();
                sr.Dispose();
            }

            return strResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReadAllFile()
        {
            string strResult = "";

            if (IsDirExist() && IsFileExist())
            {
                StreamReader sr = new StreamReader(strFilePath);
                strResult = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
           
            return strResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strContent"></param>
        public void WriteFile(string strContent)
        {
            if (!IsDirExist())
            {
                Directory.CreateDirectory(strDirPath);
            }

            byte[] byteData = Encoding.Default.GetBytes(strContent);

            using (FileStream wfile = File.OpenWrite(strFilePath))
            {
                wfile.Write(byteData, 0, byteData.Length);
                wfile.Flush();
                wfile.Close();
            }
        }

        public void WriteFile(string strContent,bool isAppend)
        {
            try
            {
                if (!IsDirExist())
                {
                    Directory.CreateDirectory(strDirPath);
                }

                StreamWriter sw = new StreamWriter(strFilePath, isAppend, Encoding.Default);
                sw.WriteLine(strContent);
                sw.Flush();
                sw.Dispose();
                sw.Close();
            }
            catch 
            { 

                
            }
        }
      
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="name">图片名字</param>
        /// <param name="Package"></param>
        public void SaveImageWithName(string name, Image Package)
        {
            string fileName = name;

            if (fileName == "")
            {
                fileName = "C40X40.bmp";
            }

            //如果文件夹没存在则创建
            if (!IsDirExist())
                CreateDir();

            string packName = strDirPath + fileName ;
            Package.Save(packName, ImageFormat.Png);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Package"></param>
        public void SaveImageWithPath(string path, Image Package)
        {
            string fileName = path;

            if (path.Length < 1)
            {
                return;
            }
            else
            {
                strDirPath = strFilePath.LastIndexOf('\\') < 0 ?"" : 
                    strFilePath.Substring(0, strFilePath.LastIndexOf('\\'));

                if (strDirPath.Length > 0 && !IsDirExist())
                    CreateDir();
                
                string packName = strDirPath + fileName;
                Package.Save(packName, ImageFormat.Png);
            }
        }

    }
}
