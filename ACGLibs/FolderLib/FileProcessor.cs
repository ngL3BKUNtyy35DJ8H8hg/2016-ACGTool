using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FolderLib
{
    public static class FileProcessor
    {
        /// <summary>
        /// Tạo thư mục theo định dạng YYYY\MM\DD
        /// </summary>
        /// <returns></returns>
        public static string CreateCurrentDateFolder()
        {
            return DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString();
        }

        /// <summary>
        /// Tạo thư mục theo định dạng YYYY\MM\YY
        /// </summary>
        /// <returns></returns>
        public static string CreateSelectedDateFolder(DateTime dt)
        {
            return dt.Year.ToString() + "\\" + dt.Month.ToString() + "\\" + dt.Day.ToString();
        }

        /// <summary>
        /// Kiểm tra đường dẫn là folder hay file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFolderPath(string path)
        {
            bool result = false;
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory) //Its a folder
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Tạo các default folder của ngày hiện tại
        /// </summary>
        public static void CreateDefaultDirectories(string rootPath)
        {
            string rootFolder = rootPath + "\\" + CreateCurrentDateFolder();
            //Current Date Folder
            string folderPath = rootFolder;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //Current Company Folder
            folderPath = rootFolder + "\\COMPANY";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //Current Personal Folder
            folderPath = rootFolder + "\\PERSONAL";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// Kiểm tra thư mục cá nhân đúng định dạng [CODE-NAME-GENDER-YEAROFBIRTH] không
        /// </summary>
        /// <param name="personalFolderName"></param>
        /// <returns></returns>
        public static string ValidatingPersonalFolder(string folderPath, string personalFolderName)
        {
            string result = "";
            try
            {
                //Kiểm tra định dạng folder Employee hợp lệ [CODE-NAME-GENDER-YEAROFBIRTH]
                string[] names = personalFolderName.Trim().Split(new char[] { '-' });
                if (names.Length == 4)
                {
                    string code = names[0].Trim();
                    if (code.Length > 15)
                    {
                        result = string.Format("Số ký tự của \"Mã bệnh nhân\" của {0} không quá 15 ký tự", folderPath);
                        return result;
                    }
                    string patientName = names[1].Trim();
                    string gender = names[2].Trim();
                    if (gender.Length > 6)
                    {
                        result = string.Format("Số ký tự của \"Giới tính\" của {0} không quá 6 ký tự", folderPath);
                        return result;
                    }
                    string yearOfBirth = names[3].Trim();
                    if (yearOfBirth.Length > 4)
                    {
                        result = string.Format("Số ký tự của \"Năm sinh\" của {0} không quá 4 ký tự", folderPath);
                        return result;
                    }
                }
                else
                {
                    result = string.Format("Thư mục bệnh nhân {0} không đúng định dạng MA-TEN-GIOITINH-NAMSINH",
                                           folderPath);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            
            return result;
        }



        /// <summary>
        /// + Kiểm tra file up lên có đúng file ảnh không
        /// </summary>
        /// <returns></returns>
        public static string ValidatingImageFile(string personalFolderPath, string personalImageFileName)
        {
            string result = "";
            try
            {
                if (IsFolderPath(personalFolderPath))
                {
                    //Nếu đúng định dạng employee folder, kiểm tra tiếp file ảnh
                    string imageFilePath = personalFolderPath + "\\" + personalImageFileName;
                    if (!IsFolderPath(imageFilePath)) //Nếu là file (có thể không là định dạng file ảnh)
                    {
                        //Lấy đuôi file
                        string[] str = personalImageFileName.Split(new char[] { '.' });
                        string fileType = str[str.Length - 1].ToLower();
                        if (fileType != "jpg" && fileType != "jpeg" && fileType != "png") //Nếu là đuôi file ảnh
                        {
                            result = string.Format("File {0} không phải là file ảnh để upload", imageFilePath);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

       /// <summary>
        /// Kiểm tra đường dẫn FILE upload lên phải theo định dạng:
        /// + Khách hàng là cty: [Year]\\[Month]\\[Day]\\COMPANY\\[Company Name]\\[Personal Name]\\[ImageFile] 
        /// + Khách hàng là cá nhân: [Day]\\PERSONAL\\[Personal Name]\\[ImageFile]
        /// Ngoài ra thư mục [Personal Name] phải đúng định dạng CODE-NAME-GENDER-YEAROFBIRTH
        /// thì mới cho upload lên server
       /// </summary>
       /// <param name="filePath"></param>
       /// <returns></returns>
        public static string ValidatingFilePath(string filePath)
        {
            string result = "";
            try
            {
                if (FileProcessor.IsFolderPath(filePath))
                {
                    result = string.Format("Path {0} là thư mục không phải file", filePath);
                    return result;
                }

                if (!File.Exists(filePath))
                {
                    result = string.Format("Path {0} không tồn tại", filePath);
                    return result;
                }

                string folderPath = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileName(filePath);
                //Kiểm tra đường dẫn folder hợp lệ
                result = ValidatingFolderPath(folderPath);
                if (result == "")
                {
                    //Kiểm tra đường dẫn có phải là file ảnh hợp lệ không
                    result = ValidatingImageFile(folderPath, fileName);
                }
                //ValidatingImageFile(folderPath, fileName);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra đường dẫn DIRECTORY phải theo định dạng
        /// + Khách hàng là cty: [Year]\\[Month]\\[Day]\\COMPANY\\[Company Name]\\[Personal Name]
        /// + Khách hàng là cá nhân: [Day]\\PERSONAL\\[Personal Name]\\[ImageFile]
        /// thì mới cho import vào CSDL
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static string ValidatingFolderPath(string folderPath)
        {
            string result = "";
            try
            {
                if (!FileProcessor.IsFolderPath(folderPath))
                {
                    result = string.Format("{0} là file không phải thư mục", folderPath);
                    return result;
                }

                if (!Directory.Exists(folderPath))
                {
                    result = string.Format("Path {0} không tồn tại", folderPath);
                    return result;
                }

                string[] paths;

                string companyPathFormat = "\\COMPANY\\";
                string personalPathFormat = "\\PERSONAL\\";
                if (folderPath.Contains(companyPathFormat)) //Nếu đường dẫn theo định dạng Company
                {
                    companyPathFormat = folderPath.Substring(0, folderPath.IndexOf(companyPathFormat)) +
                                        companyPathFormat;
                    /*Lấy đường dẫn sau \\COMPANY\\
                    Nếu đúng định dạng thì:
                    + path[0] là [Company Name]
                    + paht[1] là [Personal Name]*/
                    paths = folderPath.Replace(companyPathFormat, "").Split(new char[] { '\\' });
                    if (paths.Length == 2)
                    {
                        //Sau \\COMPANY\\ phải là thư mục [Company Name]
                        string companyFolder = companyPathFormat + paths[0];
                        if (IsFolderPath(companyFolder))
                        {
                            //Kiểm tra định dạng [Personal Name]
                            result = ValidatingPersonalFolder(folderPath, paths[1]);
                            //if (!ValidatingPersonalFolder(folderPath, paths[1]))
                            //{
                            //    result = string.Format("Thư mục bệnh nhân {0} không đúng định dạng MA-TEN-GIOITINH-NAMSINH", folderPath);
                            //}
                        }
                    }
                    else
                    {
                        result = string.Format("Đường dẫn {0} không đúng định dạng {1}[Company Name]\\[Personal Name]", folderPath, companyPathFormat);
                    }
                }
                else if (folderPath.Contains(personalPathFormat)) //Nếu đường dẫn theo định dạng Personal
                {
                    personalPathFormat = folderPath.Substring(0, folderPath.IndexOf(personalPathFormat)) +
                                         personalPathFormat;
                    /*Lấy đường dẫn sau \\PERSONAL\\
                    Nếu đúng định dạng thì:
                    + path[0] phải là [Personal Name]*/
                    paths = folderPath.Replace(personalPathFormat, "").Split(new char[] { '\\' });
                    if (paths.Length == 1)
                    {
                        //Kiểm tra định dạng [Personal Name]
                        result = ValidatingPersonalFolder(folderPath, paths[0]);
                        //if (!ValidatingPersonalFolder(folderPath, paths[0]))
                        //{
                        //    result = string.Format("Thư mục bệnh nhân {0} không đúng định dạng MA-TEN-GIOITINH-NAMSINH", folderPath);
                        //}
                    }
                    else
                    {
                        result = string.Format("Đường dẫn {0} không đúng định dạng {1}[Personal Name]", folderPath, personalPathFormat);
                    }
                }
                else
                {
                    result = string.Format("Đường dẫn {0} phải nằm trong thư mục COMPANY hoặc PERSONAL", folderPath);
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Loại bỏ ký tự "\\" ở cuối chuỗi
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static string FormatAbsoluteDirectoryPath(string folderPath)
        {
            while (folderPath.Substring(folderPath.Length - 1) == "\\")
            {
                folderPath = folderPath.Substring(0, folderPath.Length - 1);
            }
            return folderPath;
        }


        /// <summary>
        /// Tạo random pass
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Copy Folder
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
