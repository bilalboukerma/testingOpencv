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
              }

        private void button2_Click(object sender, EventArgs e)
        {            // Open File Dialog to Select Image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tiff";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string outputFolder = Path.Combine(Path.GetDirectoryName(filePath), "Output");

                    // Ensure output folder exists
                    Directory.CreateDirectory(outputFolder);

                    // Apply each technique
                    ApplyTemplateMatching(filePath, outputFolder);
                    ApplyContourDetection(filePath, outputFolder);
                    ApplyHoughCircleTransform(filePath, outputFolder);
                    // ApplyMachineLearning(filePath, outputFolder); // Requires a trained model
                }
            }
        }



        // Template Matching
        private void ApplyTemplateMatching(string imagePath, string outputFolder)
        {
            try
            {
                using (var inputImage = new Image<Bgr, byte>(imagePath))
                using (var templateImage = new Image<Bgr, byte>(@"C:\Users\pc\Desktop\BILAL\template.jpg")) // Load template image
                {
                    // Perform template matching
                    var result = inputImage.MatchTemplate(templateImage, TemplateMatchingType.CcoeffNormed);
                    result.MinMax(out double[] minValues, out double[] maxValues, out Point[] minLocations, out Point[] maxLocations);

                    // Draw rectangle around the best match
                    var matchLocation = maxLocations[0];
                    inputImage.Draw(new Rectangle(matchLocation, templateImage.Size), new Bgr(Color.Red), 2);

                    // Save results
                    string outputImagePath = Path.Combine(outputFolder, "template_matching_result.jpg");
                    inputImage.Save(outputImagePath);

                    string outputTextPath = Path.Combine(outputFolder, "template_matching_result.txt");
                    File.WriteAllText(outputTextPath, $"Best match found at: {matchLocation}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Template Matching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Contour Detection
        private void ApplyContourDetection(string imagePath, string outputFolder)
        {
            try
            {
                using (var inputImage = new Image<Bgr, byte>(imagePath))
                using (var grayImage = inputImage.Convert<Gray, byte>())
                using (var binaryImage = grayImage.ThresholdBinaryInv(new Gray(127), new Gray(255)))
                {
                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    Mat hierarchy = new Mat();
                    CvInvoke.FindContours(binaryImage, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                    List<string> contourResults = new List<string>();
                    for (int i = 0; i < contours.Size; i++)
                    {
                        var contour = contours[i];
                        var boundingBox = CvInvoke.BoundingRectangle(contour);
                        inputImage.Draw(boundingBox, new Bgr(Color.Green), 2);
                        contourResults.Add($"Contour {i + 1}: Area={CvInvoke.ContourArea(contour)}, BoundingBox={boundingBox}");
                    }

                    // Save results
                    string outputImagePath = Path.Combine(outputFolder, "contour_detection_result.jpg");
                    inputImage.Save(outputImagePath);

                    string outputTextPath = Path.Combine(outputFolder, "contour_detection_result.txt");
                    File.WriteAllLines(outputTextPath, contourResults);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Contour Detection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hough Circle Transform
        private void ApplyHoughCircleTransform(string imagePath, string outputFolder)
        {
            try
            {
                using (var inputImage = new Image<Bgr, byte>(imagePath))
                using (var grayImage = inputImage.Convert<Gray, byte>())
                {
                    var circles = CvInvoke.HoughCircles(
                        grayImage,
                        
                      HoughModes.Gradient,
                        dp: 1,
                        minDist: 20,
                        param1: 50,
                        param2: 30,
                        minRadius: 10,
                        maxRadius: 50
                    );

                    List<string> circleResults = new List<string>();
                    for (int i = 0; i < circles.Length; i++)
                    {
                        var circle = circles[i];
                        inputImage.Draw(circle, new Bgr(Color.Blue), 2);
                        circleResults.Add($"Circle {i + 1}: Center=({circle.Center.X}, {circle.Center.Y}), Radius={circle.Radius}");
                    }

                    // Save results
                    string outputImagePath = Path.Combine(outputFolder, "hough_circle_result.jpg");
                    inputImage.Save(outputImagePath);

                    string outputTextPath = Path.Combine(outputFolder, "hough_circle_result.txt");
                    File.WriteAllLines(outputTextPath, circleResults);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Hough Circle Transform: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Machine Learning (Placeholder)
        private void ApplyMachineLearning(string imagePath, string outputFolder)
        {
            try
            {
                // Load a pre-trained model (e.g., YOLO, TensorFlow)
                // Perform inference on the image
                // Save results
                MessageBox.Show("Machine Learning function is not implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Machine Learning: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}