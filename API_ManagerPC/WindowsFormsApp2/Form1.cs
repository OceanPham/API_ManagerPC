using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        //private ListView processListView;
        //private Button refreshButton;
        private ContextMenuStrip contextMenuStrip;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeControls();
            
        }

        private void InitializeControls()
        {
        
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Kích hoạt nút "btnEndTask" khi có mục được chọn
                btnEndTask.Enabled = true;
            }
            else
            {
                // Vô hiệu hóa nút "btnEndTask" khi không có mục được chọn
                btnEndTask.Enabled = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            try
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        ListViewItem item = new ListViewItem(process.ProcessName);
                        item.SubItems.Add(process.Id.ToString());
                        item.SubItems.Add(process.WorkingSet64.ToString());
                        item.SubItems.Add(process.Responding.ToString());
                        item.SubItems.Add(process.StartTime.ToString());

                        try
                        {
                            item.SubItems.Add(process.StartInfo.FileName);
                        }
                        catch (Exception)
                        {
                            item.SubItems.Add("N/A");
                        }

                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving process list: " + ex.Message, "Task Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowTaskManager_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            // Thiết lập ListView để hiển thị các cột
            listView1.View = View.Details;
            listView1.Columns.Add("Tên tiến trình", 100);
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Bộ nhớ chiếm dụng", 150);
            listView1.Columns.Add("Đơn vị", 50);
            listView1.Columns.Add("Thời gian mở", 150);
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // Xóa tất cả các mục trong ListView trước khi cập nhật danh sách chương trình
            listView1.Items.Clear();

            // Lấy danh sách các quy trình đang chạy trên máy tính
            Process[] processes = Process.GetProcesses();

            // Tạo đối tượng PerformanceCounter để lấy thông tin về bộ nhớ sử dụng và thời gian khởi động
            PerformanceCounter memCounter = new PerformanceCounter("Process", "Working Set - Private", "_Total");
            PerformanceCounter timeCounter = new PerformanceCounter("Process", "Elapsed Time", "_Total");

            // Hiển thị thông tin về từng quy trình trong ListView
            foreach (Process process in processes)
            {
                // Chỉ lấy thông tin về các ứng dụng đang chạy, bỏ qua các quy trình hệ thống và tiến trình nền
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    string processName = process.ProcessName;
                    long memoryUsage = process.WorkingSet64 / 1024 / 1024; // Đơn vị: MB
                    string startTime = process.StartTime.ToString();

                    ListViewItem item = new ListViewItem(processName);
                    item.SubItems.Add(process.Id.ToString());
                    item.SubItems.Add(memoryUsage.ToString());
                    item.SubItems.Add("MB");
                    item.SubItems.Add(startTime);
                    item.SubItems.Add(startTime);

                    listView1.Items.Add(item);
                }
            }
        }

        private void btnEndTask_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục được chọn trong ListView không
            if (listView1.SelectedItems.Count > 0)
            {

                ListViewItem selectedItem = listView1.SelectedItems[0];

                if (selectedItem != null)
                {
                    string processId = selectedItem.SubItems[1].Text;
                    string processName = selectedItem.Text;

                    DialogResult result = MessageBox.Show("Are you sure you want to end the task '" + processName + "'?", "Task Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(processId);
                            Process process = Process.GetProcessById(id);
                            process.Kill();
                            listView1.Items.Remove(selectedItem);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error ending the task: " + ex.Message, "Task Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnShowMemmory_Click(object sender, EventArgs e)
        {
            // Xóa các cột hiện tại trong ListView (nếu có)
            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Thêm các cột mới vào ListView
            listView1.View = View.Details;
            listView1.Columns.Add("Ổ đĩa", 30);
            listView1.Columns.Add("Tổng bộ nhớ (MB)", 150);
            listView1.Columns.Add("Bộ nhớ đã sử dụng (MB)", 150);
            listView1.Columns.Add("Bộ nhớ còn trống (MB)", 150);

            // Lấy danh sách tất cả các ổ đĩa trong máy tính
            DriveInfo[] drives = DriveInfo.GetDrives();

            // Duyệt qua từng ổ đĩa
            foreach (DriveInfo drive in drives)
            {
                // Kiểm tra xem ổ đĩa có sẵn hay không
                if (drive.IsReady)
                {
                    // Tên ổ đĩa
                    string driveName = drive.Name;

                    // Tổng bộ nhớ (chuyển đổi sang đơn vị GB)
                    double totalSize = drive.TotalSize / (1024 * 1024);

                    // Bộ nhớ đã sử dụng (chuyển đổi sang đơn vị GB)
                    double usedSpace = (drive.TotalSize - drive.AvailableFreeSpace) / (1024 * 1024);

                    // Bộ nhớ còn trống (chuyển đổi sang đơn vị GB)
                    double freeSpace = drive.AvailableFreeSpace / (1024 * 1024);

                    // Tạo một mục mới trong ListView với thông tin ổ đĩa
                    ListViewItem item = new ListViewItem(driveName);
                    item.SubItems.Add(totalSize.ToString("0.00"));
                    item.SubItems.Add(usedSpace.ToString("0.00"));
                    item.SubItems.Add(freeSpace.ToString("0.00"));

                    // Thêm mục mới vào ListView
                    listView1.Items.Add(item);
                }
            }

        }

        private void btnShowThisPC_Click(object sender, EventArgs e)
        {
            // Xóa các cột hiện tại trong ListView (nếu có)
            listView1.Columns.Clear();

            // Xóa các mục hiện tại trong ListView (nếu có)
            listView1.Items.Clear();

            // Thêm các cột mới vào ListView
            listView1.View = View.Details;
            listView1.Columns.Add("Tên", 100);
            listView1.Columns.Add("Loại", 100);
            listView1.Columns.Add("Kích thước", 150);
            listView1.Columns.Add("Thời gian sửa đổi", 200);

            // Lấy danh sách các thư mục gốc trong This PC
            string[] rootDirectories = Directory.GetLogicalDrives();

            // Duyệt qua từng thư mục gốc
            foreach (string directory in rootDirectories)
            {
                // Tạo một mục mới trong ListView với thông tin thư mục gốc
                ListViewItem directoryItem = new ListViewItem(directory);

                // Lấy thông tin thư mục gốc
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                directoryItem.SubItems.Add("Thư mục");
                directoryItem.SubItems.Add("");
                directoryItem.SubItems.Add(directoryInfo.LastWriteTime.ToString());

                // Gắn một thuộc tính UserState cho mục để lưu trạng thái mở rộng/collapse
                directoryItem.Tag = false; // Ban đầu mục được thu gọn

                // Thêm mục mới vào ListView
                listView1.Items.Add(directoryItem);
            }

            // Gắn sự kiện MouseDoubleClick cho ListView
            listView1.MouseDoubleClick += new MouseEventHandler(listView1_MouseDoubleClick);
        }
        public Dictionary<ListViewItem, bool> expandedStateDict;


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Lấy mục được double-click
            ListViewItem selectedItem = listView1.SelectedItems[0];
            expandedStateDict = new Dictionary<ListViewItem, bool>();
            // Kiểm tra xem mục được double-click có phải là thư mục hay không
            if (selectedItem.SubItems[1].Text == "Thư mục")
            {
                // Kiểm tra trạng thái mở rộng/collapse của mục
                bool isExpanded;
                if (expandedStateDict.ContainsKey(selectedItem))
                {
                    isExpanded = expandedStateDict[selectedItem];
                }
                else
                {
                    isExpanded = false;
                    expandedStateDict.Add(selectedItem, isExpanded);
                }

                if (isExpanded)
                {
                    // Nếu mục đã được mở rộng, thu gọn lại bằng cách xóa tất cả các mục con của mục được double-click
                    int index = listView1.Items.IndexOf(selectedItem);
                    int nextIndex = index + 1;

                    while (nextIndex < listView1.Items.Count && listView1.Items[nextIndex].SubItems[0].Text != "Thư mục")
                    {
                        listView1.Items.RemoveAt(nextIndex);
                    }

                    // Cập nhật trạng thái thu gọn của mục trong từ điển
                    expandedStateDict[selectedItem] = false;
                }
                else
                {
                    // Nếu mục đang thu gọn, mở rộng bằng cách thêm các mục con và cập nhật trạng thái mở rộng
                    // Lấy đường dẫn của thư mục được double-click
                    string directoryPath = selectedItem.Text;

                    // Lấy danh sách thư mục con và tập tin trong thư mục được double-click
                    string[] subDirectories = Directory.GetDirectories(directoryPath);
                    string[] files = Directory.GetFiles(directoryPath);

                    // Duyệt qua từng thư mục con
                    int index = listView1.Items.IndexOf(selectedItem);
                    foreach (string subDirectory in subDirectories)
                    {
                        // Tạo một mục mới trong ListView với thông tin thư mục con
                        ListViewItem subDirectoryItem = new ListViewItem(Path.GetFileName(subDirectory));

                        // Lấy thông tin thư mục con
                        DirectoryInfo subDirectoryInfo = new DirectoryInfo(subDirectory);
                        subDirectoryItem.SubItems.Add("Thư mục");
                        subDirectoryItem.SubItems.Add("");
                        subDirectoryItem.SubItems.Add(subDirectoryInfo.LastWriteTime.ToString());

                        // Thêm mục mới vào ListView
                        listView1.Items.Insert(index + 1, subDirectoryItem);
                        index++;
                    }

                    // Duyệt qua từng tập tin
                    foreach (string file in files)
                    {
                        // Tạo một mục mới trong ListView với thông tin tập tin
                        ListViewItem fileItem = new ListViewItem(Path.GetFileName(file));

                        // Lấy thông tin tập tin
                        FileInfo fileInfo = new FileInfo(file);
                        fileItem.SubItems.Add("Tập tin");
                        fileItem.SubItems.Add(fileInfo.Length.ToString());
                        fileItem.SubItems.Add(fileInfo.LastWriteTime.ToString());

                        // Thêm mục mới vào ListView
                        listView1.Items.Insert(index + 1, fileItem);
                        index++;
                    }

                    // Cập nhật trạng thái mở rộng của mục trong từ điển
                    expandedStateDict[selectedItem] = true;
                }
            }
        }

        private string sourcePath = "start"; // Biến lưu đường dẫn nguồn cần sao chép
        private string parent = "";

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn một mục trong ListView chưa
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string selectedItemName = selectedItem.SubItems[0].Text;
                string selectedItemType = selectedItem.SubItems[1].Text;
                string selectedItemPath = GetSelectedItemPath(selectedItem);

                if (selectedItemType == "Thư mục")
                {
                    // Nếu là thư mục, lưu đường dẫn nguồn cần sao chép
                    sourcePath = selectedItemPath;
                }
                else
                {
                    // Nếu là file, lưu đường dẫn tệp tin
                    sourcePath = selectedItemPath;
                    if (!string.IsNullOrEmpty(sourcePath))
                    {
                        MessageBox.Show("Sao chép thành công! Đường dẫn nguồn: " + sourcePath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private string GetSelectedItemPath(ListViewItem selectedItem)
        {
            string selectedItemName = selectedItem.SubItems[0].Text;
            string selectedItemType = selectedItem.SubItems[1].Text;
            string selectedItemPath = selectedItemName;

            int selectedItemIndex = listView1.Items.IndexOf(selectedItem);

            for (int i = selectedItemIndex - 1; i >= 0; i--)
            {
                ListViewItem currentItem = listView1.Items[i];
                string currentItemType = currentItem.SubItems[1].Text;

                if (currentItemType == "Thư mục")
                {
                    selectedItemPath = Path.Combine(currentItem.SubItems[0].Text, selectedItemPath);
                }
                else
                {
                    break;
                }
            }

            return selectedItemPath;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã có đường dẫn nguồn cần sao chép hay không
            if (!string.IsNullOrEmpty(sourcePath))
            {
                // Lấy đường dẫn đích là thư mục làm việc hiện tại
                string destinationPath = Directory.GetCurrentDirectory();

                // Kiểm tra xem đã chọn một mục trong ListView chưa
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listView1.SelectedItems[0];
                    string selectedItemType = selectedItem.SubItems[1].Text; // Kiểu (Thư mục hoặc Tập tin) của mục được chọn

                    // Kiểm tra xem mục được chọn có phải là thư mục hay không
                    if (selectedItemType == "Thư mục")
                    {
                        string selectedFolderName = selectedItem.Text; // Tên thư mục được chọn
                        destinationPath = Path.Combine(destinationPath, selectedFolderName); // Thêm tên thư mục vào đường dẫn đích
                    }
                }

                // Thực hiện sao chép
                if (Directory.Exists(sourcePath))
                {
                    // Nếu là thư mục, sao chép thư mục và toàn bộ nội dung bên trong
                    string folderName = Path.GetFileName(sourcePath);
                    string destinationFolderPath = Path.Combine(destinationPath, folderName);

                    if (!Directory.Exists(destinationFolderPath))
                    {
                        Directory.CreateDirectory(destinationFolderPath);
                    }

                    // Sao chép các tệp và thư mục con
                    CopyDirectory(sourcePath, destinationFolderPath);

                    // Hiển thị dialog thông báo
                    MessageBox.Show("Sao chép thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (File.Exists(sourcePath))
                {
                    // Nếu là file, sao chép file vào thư mục đích
                    string fileName = Path.GetFileName(sourcePath);
                    string destinationFilePath = Path.Combine(destinationPath, fileName);

                    File.Copy(sourcePath, destinationFilePath, true);

                    // Hiển thị dialog thông báo
                    MessageBox.Show("Dán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

             
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            // Sao chép các tệp và thư mục con
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDir, destinationDir));
            }

            foreach (string newPath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourceDir, destinationDir), true);
            }
        }



        private void btnShutDown_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tắt máy không?", "Xác nhận tắt máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }


        private void btnShowInforPC_Click(object sender, EventArgs e)
        {
            // Xóa các item cũ trong ListView
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.View = View.Details;
            // Tạo các cột cho ListView
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("Value", 350);

            // Truy vấn thông tin hệ điều hành
            ManagementObjectSearcher mosOS = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject moOS in mosOS.Get())
            {
                // Lấy tên hệ điều hành
                string osName = moOS.GetPropertyValue("Caption").ToString();
                // Lấy phiên bản hệ điều hành
                string osVersion = moOS.GetPropertyValue("Version").ToString();
                // Lấy số serial hệ điều hành
                string osSerial = moOS.GetPropertyValue("SerialNumber").ToString();
                // Lấy ngôn ngữ hệ điều hành
                string osLanguage = moOS.GetPropertyValue("OSLanguage").ToString();

                // Tạo một ListViewItem và gán các thông tin vào các subitem
                ListViewItem itemOS = new ListViewItem("Operating System");
                itemOS.SubItems.Add(osName + " " + osVersion + " " + osSerial + " " + osLanguage);

                // Thêm ListViewItem vào ListView
                listView1.Items.Add(itemOS);
            }

            // Truy vấn thông tin bộ xử lý
            ManagementObjectSearcher mosCPU = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject moCPU in mosCPU.Get())
            {
                // Lấy tên bộ xử lý
                string cpuName = moCPU.GetPropertyValue("Name").ToString();
                // Lấy tốc độ bộ xử lý
                string cpuSpeed = moCPU.GetPropertyValue("MaxClockSpeed").ToString() + " MHz";
                // Lấy số lõi bộ xử lý
                string cpuCores = moCPU.GetPropertyValue("NumberOfCores").ToString();
                // Lấy số luồng bộ xử lý
                string cpuThreads = moCPU.GetPropertyValue("NumberOfLogicalProcessors").ToString();

                // Tạo một ListViewItem và gán các thông tin vào các subitem
                ListViewItem itemCPU = new ListViewItem("Processor");
                itemCPU.SubItems.Add(cpuName + " " + cpuSpeed + " " + cpuCores + " cores " + cpuThreads + " threads");

                // Thêm ListViewItem vào ListView
                listView1.Items.Add(itemCPU);
            }

            // Truy vấn thông tin bộ nhớ
            ManagementObjectSearcher mosRAM = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject moRAM in mosRAM.Get())
            {
                // Lấy dung lượng bộ nhớ
                double ramSize = Convert.ToDouble(moRAM.GetPropertyValue("Capacity"));
                // Chuyển đổi đơn vị từ byte sang GB
                ramSize = ramSize / 1024 / 1024 / 1024;
                // Làm tròn đến 2 chữ số thập phân
                ramSize = Math.Round(ramSize, 2);

                // Tạo một ListViewItem và gán các thông tin vào các subitem
                ListViewItem itemRAM = new ListViewItem("Memory");
                itemRAM.SubItems.Add(ramSize.ToString() + " GB");

                // Thêm ListViewItem vào ListView
                listView1.Items.Add(itemRAM);
            }

            // Truy vấn thông tin ổ đĩa
            ManagementObjectSearcher mosDisk = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");

            foreach (ManagementObject moDisk in mosDisk.Get())
            {
                // Lấy tên ổ đĩa
                string diskName = moDisk.GetPropertyValue("DeviceID").ToString();
                // Lấy dung lượng ổ đĩa
                double diskSize = Convert.ToDouble(moDisk.GetPropertyValue("Size"));
                // Chuyển đổi đơn vị từ byte sang GB
                diskSize = diskSize / 1024 / 1024 / 1024;
                // Làm tròn đến 2 chữ số thập phân
                diskSize = Math.Round(diskSize, 2);

                // Tạo một ListViewItem và gán các thông tin vào các subitem
                ListViewItem itemDisk = new ListViewItem("Disk");
                itemDisk.SubItems.Add(diskName + " " + diskSize.ToString() + " GB");

                // Thêm ListViewItem vào ListView
                listView1.Items.Add(itemDisk);
            }

            ManagementObjectSearcher mosComputerSystemProduct = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
            foreach (ManagementObject moComputerSystemProduct in mosComputerSystemProduct.Get())
            {

                string productID = string.Empty;
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        productID = obj["SerialNumber"].ToString().Trim();
                        break; // Chỉ lấy thông tin từ một đối tượng đầu tiên
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                ListViewItem product_ID = new ListViewItem("Product_ID");
                product_ID.SubItems.Add(productID.ToString());
                listView1.Items.Add(product_ID);


                string version = string.Empty;

                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                    ManagementObjectCollection collection = searcher.Get();

                    foreach (ManagementObject obj in collection)
                    {
                        version = obj["Version"] as string;
                        break; // Chỉ lấy thông tin từ một đối tượng đầu tiên
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                ListViewItem version_PC = new ListViewItem("Version_PC");
                version_PC.SubItems.Add(version.ToString());
                listView1.Items.Add(version_PC);
                Console.WriteLine(version);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tắt máy không?", "Xác nhận tắt máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }

        private void btnCreateProccess_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một đối tượng ProcessStartInfo để cấu hình thông tin của quy trình
                ProcessStartInfo startInfo = new ProcessStartInfo();

                // Đặt tên của chương trình muốn chạy
                startInfo.FileName = "notepad.exe";

                // Khởi tạo một đối tượng Process để đại diện cho quy trình
                Process process = new Process();

                // Gán thông tin cấu hình cho quy trình từ đối tượng ProcessStartInfo
                process.StartInfo = startInfo;

                // Khởi chạy quy trình
                process.Start();

                Console.WriteLine("Quy trình đã được khởi chạy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }

            Console.ReadLine();
        }

        private void ptbLock_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn khóa máy không?", "Xác nhận khóa máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Process.Start("rundll32.exe", "user32.dll,LockWorkStation");
            }
        }
    }
}