using System.IO.Packaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Python.Runtime;
using System.Runtime.InteropServices;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupOpenCV();
        }

        private void SetupOpenCV()
        {
            try
            {
                // Get the application base directory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string dllDir = Path.Combine(baseDir, "runtimes", "win-x64", "native");

                if (!Directory.Exists(dllDir))
                {
                    MessageBox.Show($"OpenCV DLL directory not found at: {dllDir}");
                    return;
                }

                // Add to PATH environment variable
                Environment.SetEnvironmentVariable("PATH",
                    Environment.GetEnvironmentVariable("PATH") + ";" + dllDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up OpenCV: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show the OpenFileDialog to select an image

    /*        if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog1.FileName;

                // Load the image using OpenCV
                OpenCvSharp.Mat image = Cv2.ImRead(filePath, OpenCvSharp.ImreadModes.Color);
                OpenCvSharp.Mat gray = new OpenCvSharp.Mat();
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

                // Apply binary thresholding
                OpenCvSharp.Mat thresh = new OpenCvSharp.Mat();
                Cv2.Threshold(gray, thresh, 150, 255, ThresholdTypes.BinaryInv);

                // Find contours
                // Use OpenCvSharp.Point explicitly
                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;
                Cv2.FindContours(thresh, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                // Define bubble area range (adjust based on your image)
                int minBubbleArea = 100;
                int maxBubbleArea = 1000;

                // Loop through contours and detect filled bubbles
                foreach (var contour in contours)
                {
                    double area = Cv2.ContourArea(contour);
                    if (area > minBubbleArea && area < maxBubbleArea)
                    {
                        // Create a mask for the bubble
                        OpenCvSharp.Mat mask = OpenCvSharp.Mat.Zeros(gray.Size(), MatType.CV_8UC1);
                        Cv2.DrawContours(mask, new OpenCvSharp.Point[][] { contour }, -1, Scalar.White, -1);

                        // Calculate the percentage of filled pixels
                        int filledPixels = Cv2.CountNonZero(mask);
                        double fillPercentage = (filledPixels / area) * 100;

                        // Determine if the bubble is filled
                        if (fillPercentage > 50) // Adjust threshold as needed
                        {
                            Console.WriteLine("Bubble is filled");
                        }
                        else
                        {
                            Console.WriteLine("Bubble is not filled");
                        }
                    }
                }

                // Display the thresholded image in the PictureBox
                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(thresh);
            }
     */   }

        private void button2_Click(object sender, EventArgs e)
        {/*
            // Show the OpenFileDialog to select an image
            // Show the OpenFileDialog to select an image
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog1.FileName;

                // Load the image using Emgu CV
                Mat image = CvInvoke.Imread(filePath, ImreadModes.Color);
                Mat gray = new Mat();
                CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);

                // Apply binary thresholding
                Mat thresh = new Mat();
                CvInvoke.Threshold(gray, thresh, 150, 255, ThresholdType.BinaryInv);

                // Find contours
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hierarchy = new Mat();
                CvInvoke.FindContours(thresh, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                // Define bubble area range (adjust based on your image)
                int minBubbleArea = 100;
                int maxBubbleArea = 1000;

                // Loop through contours and detect filled bubbles
                for (int i = 0; i < contours.Size; i++)
                {
                    double area = CvInvoke.ContourArea(contours[i]);
                    if (area > minBubbleArea && area < maxBubbleArea)
                    {
                        // Create a mask for the bubble
                        Mat mask = new Mat(thresh.Size, DepthType.Cv8U, 1);
                        mask.SetTo(new MCvScalar(0));
                        CvInvoke.DrawContours(mask, contours, i, new MCvScalar(255), -1);

                        // Calculate the percentage of filled pixels
                        int filledPixels = CvInvoke.CountNonZero(mask);
                        double fillPercentage = (filledPixels / area) * 100;

                        // Determine if the bubble is filled
                        if (fillPercentage > 50) // Adjust threshold as needed
                        {
                            Console.WriteLine("Bubble is filled");
                        }
                        else
                        {
                            Console.WriteLine("Bubble is not filled");
                        }
                    }
                }

                // Convert the thresholded image to a Bitmap and display it in the PictureBox
                using (Image<Gray, byte> img = thresh.ToImage<Gray, byte>())
                {
                    pictureBox1.Image = img.Bitmap; // Use the Bitmap property
                }
            }*/
        }
 
    
    
    }
}