using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.BetterFolderBrowserNS;

namespace VideoStaganographyImtiaz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                lblselectStatus.Visible = false;
                btnEmbed.Enabled = false;
                btnRetrieve.Enabled = false;

                txtEmbedSecretMessage.MaxLength = 1000000;
                txtRetrieveSecretMessage.MaxLength = 1000000;
                txtLog.MaxLength = 1000000;


                if (!Directory.Exists(mainPath))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(mainPath);
                }

                if (!Directory.Exists(stegoFrameStore))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(stegoFrameStore);
                }

                if (!Directory.Exists(allFrames))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(allFrames);
                }

                if (!Directory.Exists(allFramesStego))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(allFramesStego);
                }
                if (!Directory.Exists(allCombineFrames))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(allCombineFrames);
                }
            }
            catch (IOException ioex)
            {
                MessageBox.Show("" + ioex.Message);
            }
        }



        public string[] selectedFolders;
        public string videoFilePath = "";
        public string mainPath = @"C:\VideoSteganography";
        public string allFrames = @"C:\VideoSteganography\allFrames";
        public string allFramesStego = @"C:\VideoSteganography\allFramesStego";
        public string stegoFrameStore = @"C:\VideoSteganography\stegoFrameStore";
        public string allCombineFrames = @"C:\VideoSteganography\allCombinedFrames";
        public string embedTime = "";
        public int frameNo = 0;
        public string message = "";
        public string messageBinary = "";
        int bitno = 0;


        private void btnSelectCoverVideo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";

                openFileDialog.Filter = "All Media Files|*.avi";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    videoFilePath = openFileDialog.FileName;
                }

                if (!String.IsNullOrEmpty(videoFilePath))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //ffmpeg -i C:\VideoSteganography\Cover.avi -vf fps=30 C:\VideoSteganography\allFrames\%06d.bmp
                    //C:\\VideoSteganography\\video.avi
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C ffmpeg -i " + videoFilePath + " -vf fps=25 C:\\VideoSteganography\\allFrames\\%06d.bmp";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    Cursor.Current = Cursors.Default;


                    int frames = (Directory.GetFiles(@"C:\VideoSteganography\allFrames")).Length;


                    pictureCover.ImageLocation = @"C:\VideoSteganography\allFrames\000001.bmp";
                    pictureCover.SizeMode = PictureBoxSizeMode.Zoom;

                    btnEmbed.Enabled = true;
                    //Random rnd = new Random();
                    //frameNo = rnd.Next(0, frames);
                    //txtFrameSelected.Text = (frameNo).ToString();

                    /*
                     * our embedable pixels number are 262044 into (512x512) so total number of secret bit will be 262044*3 = 786132 bit 
                     * that means 98Kb data into 1bit LSB position
                     */

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }

        }

        private Color rgba(Bitmap imag, int x1, int y1, int smbp)
        {
            Color pixel = imag.GetPixel(x1, y1);
            string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
            string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
            string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
            //here we are replacing the secrect message bit into the last position in red and converted to integer
            int newred = Convert.ToInt32((new StringBuilder(red) { [7] = mLengthBinaryChar[smbp + 0] }.ToString()), 2);
            int newgreen = Convert.ToInt32((new StringBuilder(green) { [7] = mLengthBinaryChar[smbp + 1] }.ToString()), 2);
            int newblue = Convert.ToInt32((new StringBuilder(blue) { [7] = mLengthBinaryChar[smbp + 2] }.ToString()), 2);
            Color myRgbColor = new System.Drawing.Color();
            myRgbColor = Color.FromArgb(pixel.A, newred, newgreen, newblue);
            return myRgbColor;
        }

        private Color rgbaPerFrame(Bitmap imag, int x1, int y1, int smbp)
        {
            Color pixel = imag.GetPixel(x1, y1);
            string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
            string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
            string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
            //here we are replacing the secrect message bit into the last position in red and converted to integer
            int newred = Convert.ToInt32((new StringBuilder(red) { [7] = messageLengthBinaryChar[smbp + 0] }.ToString()), 2);
            int newgreen = Convert.ToInt32((new StringBuilder(green) { [7] = messageLengthBinaryChar[smbp + 1] }.ToString()), 2);
            int newblue = Convert.ToInt32((new StringBuilder(blue) { [7] = messageLengthBinaryChar[smbp + 2] }.ToString()), 2);
            //set the new color to image
            Color myRgbColor = new System.Drawing.Color();
            myRgbColor = Color.FromArgb(pixel.A, newred, newgreen, newblue);
            return myRgbColor;
        }

        char[] mLengthBinaryChar;
        char[] messageLengthBinaryChar;
        private void btnEmbed_Click(object sender, EventArgs e)
        {
            try
            {
                /*here 512x512 dimentioned image has 262144 pixels where we skipd 4 rows which are edges sides. so our embed pixels will be 
                 * 512x512 = 262144 - (512x4-4) = 262044 for each frames except metadata frame. 
                */

                #region message convert to binary
                //here get the message from user
                if (!String.IsNullOrEmpty(txtEmbedSecretMessage.Text.Trim()))
                {
                    message = txtEmbedSecretMessage.Text.Trim();
                }

                //message is converted to 8bit binary
                StringBuilder sb = new StringBuilder();
                foreach (char c in message.ToCharArray())
                {
                    sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                }
                messageBinary = sb.ToString();

                //to maintain error from pass length we have to add (extra 0 or 00)
                if (((messageBinary.Length) % 3) == 2)
                {
                    messageBinary += "0";
                }
                else if (((messageBinary.Length) % 3) == 1)
                {
                    messageBinary += "00";
                }

                char[] passE = messageBinary.ToCharArray(); //all binary bit has been converted into array.

                //Console.WriteLine(messageBinary);
                #endregion

                /*
                 * our embedable pixels number are 262044 into (512x512) so total number of secret bit will be 262044*3 = 786132 bit 
                 * that means 98Kb data into 1bit LSB position
                 */
                //so our required numbers of frames will be (24/786132) = 0.00003052922 means 1 frame and 1 frame will be for meta data. means 2 frame.
                float a = messageBinary.Length / 786132;
                int requiredFrames = Convert.ToInt32(Math.Ceiling(a)) + 1;

                /*
                 * 
                 * 
                 * 
                 * *
                 */

                #region conceal the secret message size into 1 no frame with 8 pixel

                //we can store 1,67,77,215 bytes into 8 pixel
                //================================================
                //================================================
                //height video frame can be 4095 i mean 2.5 min max video length
                //we are storing this info into first frame of video
                Bitmap imag = new Bitmap(allFrames + @"\000001.bmp");


                int mLength = Convert.ToInt32(message.Length);
                string mLengthBinary = Convert.ToString(mLength, 2).PadLeft(24, '0');
                mLengthBinaryChar = mLengthBinary.ToCharArray();



                //for 1st pixel (x1,y1) = {((w/2)-2),1}
                int x1 = Convert.ToInt32((((imag.Width) / 2) - 2));
                int y1 = Convert.ToInt32(1);
                Color myRgbColor = this.rgba(imag, x1, y1, 0); //image, dimensionX, dimensionY, arrayPositionOfmLengthBinaryChar
                imag.SetPixel(x1, y1, myRgbColor);

                //for 2nd pixel 
                int x2 = Convert.ToInt32(imag.Width - 1);
                int y2 = Convert.ToInt32((imag.Height / 2) - 2);
                myRgbColor = this.rgba(imag, x2, y2, 3);
                imag.SetPixel(x2, y2, myRgbColor);

                //for 3rd pixel 
                int x3 = Convert.ToInt32((imag.Width / 2) + 2);
                int y3 = Convert.ToInt32(imag.Height - 1);
                myRgbColor = this.rgba(imag, x3, y3, 6);
                imag.SetPixel(x3, y3, myRgbColor);

                //for fourth pixel
                int x4 = Convert.ToInt32(1);
                int y4 = Convert.ToInt32((imag.Height / 2) + 2);
                myRgbColor = this.rgba(imag, x4, y4, 9);
                imag.SetPixel(x4, y4, myRgbColor);


                //for 5th pixel (x1,y1) = (0,0)
                int x5 = Convert.ToInt32(0);
                int y5 = Convert.ToInt32(0);
                myRgbColor = this.rgba(imag, x5, y5, 12);
                imag.SetPixel(x5, y5, myRgbColor);

                //for 6th pixel (0, 512)
                int x6 = Convert.ToInt32(0);
                int y6 = Convert.ToInt32(imag.Height - 1);
                myRgbColor = this.rgba(imag, x6, y6, 15);
                imag.SetPixel(x6, y6, myRgbColor);

                //for 7th pixel (512, 0)
                int x7 = Convert.ToInt32(imag.Width - 1);
                int y7 = Convert.ToInt32(0);
                myRgbColor = this.rgba(imag, x7, y7, 18);
                imag.SetPixel(x7, y7, myRgbColor);

                //for 8th pixel (512, 512)
                int x8 = Convert.ToInt32(imag.Width - 1);
                int y8 = Convert.ToInt32(imag.Height - 1);
                myRgbColor = this.rgba(imag, x8, y8, 21);
                imag.SetPixel(x8, y8, myRgbColor);


                imag.Save(stegoFrameStore + @"\000001.bmp", ImageFormat.Bmp);

                #endregion


                /*
                 * 
                 * 
                 * 
                 * *
                 */
                //this for loop is used to get the secret selected required frames 
                for (int i = 0; i < requiredFrames; i++)
                {
                    //we will take sequentially 2 to n th frames
                    using (Bitmap img = new Bitmap(allFrames + @"\" + (Convert.ToInt16(i + 2).ToString("D6") + ".bmp")))
                    {
                        #region Hiding message length into secret Four Pixel 
                        //====================================
                        //===================================
                        //here we are hiding message length
                        int messageLength = message.Length;
                        string messageLengthBinary = Convert.ToString(messageLength, 2).PadLeft(12, '0'); ;
                        messageLengthBinaryChar = messageLengthBinary.ToCharArray();

                        //for 1st pixel (x1,y1) = (0,0)
                        x1 = Convert.ToInt32(0);
                        y1 = Convert.ToInt32(0);
                        myRgbColor = this.rgbaPerFrame(img, x1, y1, 0); //image, dimensionX, dimensionY, arrayPositionOfmessageLengthBinaryChar
                        img.SetPixel(x1, y1, myRgbColor);

                        //for 2nd pixel (0, 512)
                        x2 = Convert.ToInt32(0);
                        y2 = Convert.ToInt32(img.Height - 1);
                        myRgbColor = this.rgbaPerFrame(img, x2, y2, 3);
                        img.SetPixel(x2, y2, myRgbColor);

                        //for 3rd pixel (512, 0)
                        x3 = Convert.ToInt32(img.Width - 1);
                        y3 = Convert.ToInt32(0);
                        myRgbColor = this.rgbaPerFrame(img, x3, y3, 6);
                        img.SetPixel(x3, y3, myRgbColor);

                        //for 4th pixel (512, 512)
                        x4 = Convert.ToInt32(img.Width - 1);
                        y4 = Convert.ToInt32(img.Height - 1);
                        myRgbColor = this.rgbaPerFrame(img, x4, y4, 9);
                        img.SetPixel(x4, y4, myRgbColor);
                        #endregion

                        #region Embed Secret Data into Per Frames
                        for (int w = 1; w < img.Width - 1; w++)
                        {
                            for (int h = 1; h < img.Width - 1; h++)
                            {
                                int x = Convert.ToInt32(w);
                                int y = Convert.ToInt32(h);

                                Color pixel = img.GetPixel(x, y);
                                string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
                                string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
                                string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');

                                //here we are replacing the secrect message bit into the last position in red and converted to integer
                                int newred = Convert.ToInt32((new StringBuilder(red) { [7] = passE[bitno + 0] }.ToString()), 2);
                                int newgreen = Convert.ToInt32((new StringBuilder(green) { [7] = passE[bitno + 1] }.ToString()), 2);
                                int newblue = Convert.ToInt32((new StringBuilder(blue) { [7] = passE[bitno + 2] }.ToString()), 2);

                                //set the new color to image
                                myRgbColor = new System.Drawing.Color();
                                myRgbColor = System.Drawing.Color.FromArgb(pixel.A, newred, newgreen, newblue);
                                img.SetPixel(x, y, myRgbColor);

                                bitno = bitno + 3; //this bitno will help me to know the tract of message character index

                                if (passE.Length <= bitno) break;
                            }
                            if (passE.Length <= bitno) break;
                        }
                        #endregion

                        #region Save Stego Frame
                        img.Save(stegoFrameStore + @"\" + Convert.ToInt16(i + 2).ToString("D6") + ".bmp", ImageFormat.Bmp);
                        #endregion
                    }
                }

                #region Save Stego Video 

                string saveVideoFolder = "";
                var betterFolderBrowser = new BetterFolderBrowser();

                betterFolderBrowser.Title = "Select folder to save Stego Video...";
                betterFolderBrowser.RootFolder = "C:\\";

                // Allow multi-selection of folders.
                betterFolderBrowser.Multiselect = false;

                if (betterFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolders = betterFolderBrowser.SelectedFolder;

                    saveVideoFolder = selectedFolders;
                    // If you've disabled multi-selection, use 'SelectedFolder'.
                    // string selectedFolder = betterFolderBrowser.SelectedFolder;
                }

                Cursor.Current = Cursors.WaitCursor;

                string[] allFramesForVideo = Directory.GetFiles(allFrames);
                foreach (string s in allFramesForVideo)
                {
                    string fileName = System.IO.Path.GetFileName(s);
                    string destFile = System.IO.Path.Combine(allCombineFrames + @"\", fileName);
                    System.IO.File.Copy(s, destFile, true);
                }

                string[] allFramesForVideoStegoFile = Directory.GetFiles(stegoFrameStore);
                foreach (string s in allFramesForVideoStegoFile)
                {
                    string fileName = System.IO.Path.GetFileName(s);
                    string destFile = System.IO.Path.Combine(allCombineFrames + @"\", fileName);
                    System.IO.File.Copy(s, destFile, true);
                }


                //string fileName2 = frameNo.ToString("D6") + ".bmp";
                //string destFile2 = System.IO.Path.Combine(allCombineFrames + @"\", fileName2);
                //System.IO.File.Copy(stegoFrameStore + @"\" + frameNo.ToString("D6") + ".bmp", destFile2, true);

                //fileName2 = "000001.bmp";
                //destFile2 = System.IO.Path.Combine(allCombineFrames + @"\", fileName2);
                //System.IO.File.Copy(stegoFrameStore + @"\000001.bmp", destFile2, true);



                //Cursor.Current = Cursors.WaitCursor;
                //ffmpeg -i C:\VideoSteganography\Cover.avi -vf fps=30 C:\VideoSteganography\allFrames\%06d.bmp
                //C:\\VideoSteganography\\video.avi

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                var command = "/C ffmpeg -i C:\\VideoSteganography\\allCombinedFrames\\%06d.bmp -pix_fmt bgr24 -c:v libx264rgb -preset veryslow -qp 0 C:\\VideoSteganography\\stegovideo.avi";

                startInfo.Arguments = command;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();



                //copy stego video and paste on user destination
                string vfileName = System.IO.Path.GetFileName("C:\\VideoSteganography\\stegovideo.avi");
                string vdestFile = System.IO.Path.Combine(saveVideoFolder + @"\", vfileName);
                System.IO.File.Copy("C:\\VideoSteganography\\stegovideo.avi", vdestFile, true);

                #region Measurement Metric Log
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    double mseGray = 0.0, mse = 0.0, psnr = 0.0;
                    int fNo = 1;
                    foreach (string s in allFramesForVideoStegoFile)
                    {
                        Bitmap bmp1 = new Bitmap(allFrames + @"\" + Path.GetFileName(s));
                        Bitmap bmp2 = new Bitmap(stegoFrameStore + @"\" + Path.GetFileName(s));

                        for (int i = 0; i < bmp1.Width; i++)
                        {
                            for (int j = 0; j < bmp1.Height; j++)
                            {
                                int gray1 = bmp1.GetPixel(i, j).R;
                                int gray2 = bmp2.GetPixel(i, j).R;
                                double sum = Math.Pow(gray1 - gray2, 2);
                                mseGray += sum;
                            }
                        }
                        mse = (mseGray) / (bmp1.Width * bmp1.Height) * 3;
                        psnr = 10 * Math.Log10((255 * 255) / mse);


                        string resultLog = txtLog.Text.Trim() + Environment.NewLine;
                        resultLog += fNo.ToString() + " No Frames Result: " + Environment.NewLine;
                        resultLog += "  " + "MSE: " + mse.ToString() + Environment.NewLine;
                        resultLog += "  " + "PSNR: " + psnr.ToString() + Environment.NewLine;
                        fNo++;

                        mseGray = 0.0;
                        mse = 0.0;
                        psnr = 0.0;
                        txtLog.Text = resultLog;
                        Thread.Sleep(1000);

                    }

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                #endregion

                MessageBox.Show("Successfully Saved The Stego Video", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor.Current = Cursors.Default;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSelectStegoVideo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";

                openFileDialog.Filter = "All Media Files|*.avi";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    videoFilePath = openFileDialog.FileName;
                }

                if (!String.IsNullOrEmpty(videoFilePath))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //ffmpeg -i C:\VideoSteganography\Cover.avi -vf fps=30 C:\VideoSteganography\allFrames\%06d.bmp
                    //C:\\VideoSteganography\\video.avi
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C ffmpeg -i " + videoFilePath + " -vf fps=25 C:\\VideoSteganography\\allFramesStego\\%06d.bmp";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    Cursor.Current = Cursors.Default;


                    //int frames = (Directory.GetFiles(@"C:\VideoSteganography\allFrames")).Length;


                    pictureStego.ImageLocation = @"C:\VideoSteganography\allFramesStego\000001.bmp";
                    pictureStego.SizeMode = PictureBoxSizeMode.Zoom;

                    btnRetrieve.Enabled = true;

                    /*
                     * our embedable pixels number are 262044 into (512x512) so total number of secret bit will be 262044*3 = 786132 bit 
                     * that means 98Kb data into 1bit LSB position
                     */
                    /*
                     * we are able to conceal 170 frames (16777215 bytes)
                     */

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex);
            }
        }

        private string secretBitMetaData(Bitmap img, int x, int y)
        {
            Color pixel = img.GetPixel(x, y);
            string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
            string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
            string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');
            string s = (red.Last().ToString() + green.Last().ToString() + blue.Last().ToString());
            return s;
        }


        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                string frameNoBinary = "";
                string messageLengthBinary = "";
                string secretMessage = "";
                //at first we are taking selected frames no where we have kept secrect message
                Bitmap img = new Bitmap(allFramesStego + @"\000001.bmp");

                //int x1, x2, x3, x4, y1, y2, y3, y4;

                #region Get Secret Message Length number from 1st frame with 8 pixels

                //for 1st pixel
                int x1 = Convert.ToInt32((((img.Width) / 2) - 2));
                int y1 = Convert.ToInt32(1);
                string s = secretBitMetaData(img, x1, y1);
                frameNoBinary += s;


                //for 2nd pixel 
                int x2 = Convert.ToInt32(img.Width - 1);
                int y2 = Convert.ToInt32((img.Height / 2) - 2);
                s = secretBitMetaData(img, x2, y2);
                frameNoBinary += s;

                //for 3rd pixel 
                int x3 = Convert.ToInt32((img.Width / 2) + 2);
                int y3 = Convert.ToInt32(img.Height - 1);
                s = secretBitMetaData(img, x3, y3);
                frameNoBinary += s;

                //for 4th pixel 
                int x4 = Convert.ToInt32(1);
                int y4 = Convert.ToInt32((img.Height / 2) + 2);
                s = secretBitMetaData(img, x4, y4);
                frameNoBinary += s;

                //for 5th pixel
                int x5 = Convert.ToInt32(0);
                int y5 = Convert.ToInt32(0);
                s = secretBitMetaData(img, x5, y5);
                frameNoBinary += s;


                //for 6th pixel 
                int x6 = Convert.ToInt32(0);
                int y6 = Convert.ToInt32(img.Height - 1);
                s = secretBitMetaData(img, x6, y6);
                frameNoBinary += s;

                //for 7th pixel 
                int x7 = Convert.ToInt32(img.Width - 1);
                int y7 = Convert.ToInt32(0);
                s = secretBitMetaData(img, x7, y7);
                frameNoBinary += s;

                //for 8th pixel 
                int x8 = Convert.ToInt32(img.Width - 1);
                int y8 = Convert.ToInt32(img.Height - 1);
                s = secretBitMetaData(img, x8, y8);
                frameNoBinary += s;


                //here we got the frame no from the 4 pixel of that 000001.bmp
                int frameN = Convert.ToInt32(frameNoBinary, 2);

                float a = (frameN * 8) / 786132;
                int requiredFrames = Convert.ToInt32(Math.Ceiling(a)) + 1;
                #endregion

                #region Retrieve Data From Frames
                int substract = 0;
                for (int i = 0; i < requiredFrames; i++)
                {
                    //we will take sequentially 2 to n th frames
                    using (img = new Bitmap(allFramesStego + @"\" + (Convert.ToInt16(i + 2).ToString("D6") + ".bmp")))
                    {
                        #region Hiding message length into secret Four Pixel 
                        messageLengthBinary = "";

                        //for 1st pixel
                        x1 = Convert.ToInt32(0);
                        y1 = Convert.ToInt32(0);
                        s = secretBitMetaData(img, x1, y1);
                        messageLengthBinary += s;


                        //for 2nd pixel 
                        x2 = Convert.ToInt32(0);
                        y2 = Convert.ToInt32(img.Height - 1);
                        s = secretBitMetaData(img, x2, y2);
                        messageLengthBinary += s;

                        //for 3rd pixel 
                        x3 = Convert.ToInt32(img.Width - 1);
                        y3 = Convert.ToInt32(0);
                        s = secretBitMetaData(img, x3, y3);
                        messageLengthBinary += s;

                        //for 4th pixel 
                        x4 = Convert.ToInt32(img.Width - 1);
                        y4 = Convert.ToInt32(img.Height - 1);
                        s = secretBitMetaData(img, x4, y4);
                        messageLengthBinary += s;

                        int totalMessageBit = Convert.ToInt32(messageLengthBinary, 2) * 8;

                        if (((totalMessageBit) % 3) == 2)
                        {
                            totalMessageBit += 1;
                            substract = 1;
                        }
                        else if (((totalMessageBit) % 3) == 1)
                        {
                            totalMessageBit += 2;
                            substract = 2;
                        }

                        int totalPixelPerFrame = totalMessageBit / 3;
                        #endregion

                        #region retrive Secret Data into Per Frames

                        int pixelTrack = 0;
                        for (int w = 1; w < img.Width - 1; w++)
                        {
                            for (int h = 1; h < img.Width - 1; h++)
                            {
                                int x = Convert.ToInt32(w);
                                int y = Convert.ToInt32(h);

                                Color pixel = img.GetPixel(x, y);
                                string red = Convert.ToString(pixel.R, 2).PadLeft(8, '0');
                                string green = Convert.ToString(pixel.G, 2).PadLeft(8, '0');
                                string blue = Convert.ToString(pixel.B, 2).PadLeft(8, '0');

                                secretMessage += red.Last().ToString() + green.Last().ToString() + blue.Last().ToString();

                                pixelTrack++;

                                if (totalPixelPerFrame <= pixelTrack) break;
                            }
                            if (totalPixelPerFrame <= pixelTrack) break;
                        }
                        #endregion
                    }
                }
                #endregion

                secretMessage = string.Concat(secretMessage.Reverse().Skip(substract).Reverse());

                char[] secretMessageBinary = secretMessage.ToCharArray();
                string bit8 = "";
                int aa = 0;
                string secretRealMessage = "";

                //Console.WriteLine(secretMessage);
                for (int i = 0; i < secretMessageBinary.Length; i++)
                {
                    if (aa != 8)
                    {
                        bit8 = bit8 + secretMessageBinary[i].ToString();

                        aa++;
                    }
                    if (aa == 8)
                    {
                        int acii = Convert.ToInt32(bit8, 2);
                        secretRealMessage = secretRealMessage + Char.ConvertFromUtf32(acii);
                        bit8 = "";
                        aa = 0;
                    }
                }

                txtRetrieveSecretMessage.Text = secretRealMessage;
                MessageBox.Show("Retrieved Secret message", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTextFile_Click(object sender, EventArgs e)
        {
            try
            {
                string location = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";

                openFileDialog.Filter = "All Media Files|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    location = openFileDialog.FileName;
                }

                if (String.IsNullOrEmpty(location))
                    return;

                string[] a = File.ReadAllLines(location);
                foreach (string l in a)
                {
                    message += l;
                }
                lblselectStatus.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string saveTextFolder = "";
                var betterFolderBrowser = new BetterFolderBrowser();

                betterFolderBrowser.Title = "Select folder to save Text File...";
                betterFolderBrowser.RootFolder = "C:\\";

                // Allow multi-selection of folders.
                betterFolderBrowser.Multiselect = false;

                if (betterFolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = betterFolderBrowser.SelectedFolder;

                    saveTextFolder = selectedFolder;
                    // If you've disabled multi-selection, use 'SelectedFolder'.
                    // string selectedFolder = betterFolderBrowser.SelectedFolder;
                }

                if (String.IsNullOrEmpty(saveTextFolder))
                    return;

                string createText = txtRetrieveSecretMessage.Text.Trim() + Environment.NewLine;
                File.WriteAllText(saveTextFolder + @"/SecretTextFile.txt", createText);
                MessageBox.Show("Successfully Downloaded!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
