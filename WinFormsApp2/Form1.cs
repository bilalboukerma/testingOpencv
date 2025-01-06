using System.IO.Packaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Python.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {

        string filePath = "";
        string outputFolder = "";
        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tiff";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    outputFolder = Path.Combine(Path.GetDirectoryName(filePath), "Output");

                    // Ensure output folder exists
                    Directory.CreateDirectory(outputFolder);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {            // Open File Dialog to Select Image
            // Determine which method to use based on the selected radio button
            if (radioHough.Checked)
            {
                ApplyHoughCircleTransform(filePath, outputFolder);
            }
            else if (radioTemplate.Checked)
            {
                ApplyTemplateMatching(filePath, outputFolder);
            }
            else if (radioContour.Checked)
            {
                ApplyContourDetection(filePath, outputFolder);
            }
            else if (radioHybrid.Checked)
            {
                DetectAnswers(filePath, outputFolder);
            }
            else
            {
                MessageBox.Show("Please select a method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    pictureBox2.Image = Image.FromFile(outputTextPath);
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
                    pictureBox2.Image = Image.FromFile(outputTextPath);
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
                    pictureBox2.Image = Image.FromFile(outputTextPath);
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
        private void DetectAnswers(string imagePath, string outputFolder)
        {
            try
            {
                using (var inputImage = new Image<Bgr, byte>(imagePath))
                using (var grayImage = inputImage.Convert<Gray, byte>())
                using (var binaryImage = grayImage.ThresholdBinaryInv(new Gray(127), new Gray(255)))
                {
                    // Find contours (circles)
                    VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                    Mat hierarchy = new Mat();
                    CvInvoke.FindContours(binaryImage, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                    // Group contours by their Y-coordinate (rows) and X-coordinate (columns)
                    var detectedCircles = new List<(int X, int Y, double Area, int Row, int Col)>();
                    foreach (var contour in contours.ToArrayOfArray())
                    {
                        using (var contourVector = new VectorOfPoint(contour))
                        {
                            double contourArea = CvInvoke.ContourArea(contourVector);
                            var boundingBox = CvInvoke.BoundingRectangle(contour);
                            double perimeter = CvInvoke.ArcLength(contourVector, true);
                            double circularity = 4 * Math.PI * (contourArea / (perimeter * perimeter));

                            // Filter contours based on area and circularity
                            if (contourArea > 250 && circularity > 0.7 && circularity <= 1.2)
                            {
                                // Check if the circle is filled (mean intensity is low)
                                using (var roi = new Mat(grayImage.Mat, boundingBox))
                                {
                                    MCvScalar mean = CvInvoke.Mean(roi);
                                    if (mean.V0 < 170) // Adjust threshold if needed
                                    {
                                        int row = (boundingBox.Y / (boundingBox.Height + 10));  // Row number
                                        int col = (boundingBox.X / (boundingBox.Width + 10));   // Column number
                                        detectedCircles.Add((boundingBox.X, boundingBox.Y, contourArea, row, col));
                                        CvInvoke.Rectangle(inputImage, boundingBox, new MCvScalar(0, 255, 0), 2);
                                        CvInvoke.PutText(inputImage, $"Q{row + 1}>{col + 1}", new Point(boundingBox.X, boundingBox.Y - 10),
                                            FontFace.HersheySimplex, 0.5, new MCvScalar(255, 0, 0), 1);
                                    }
                                }
                            }
                        }
                    }

                    // Group detected circles by question (row)
                    var results = detectedCircles
                        .GroupBy(c => c.Row)
                        .OrderBy(g => g.Key)
                        .Select(g => $"qs{g.Key + 1}>{g.First().Col + 1}")
                        .ToList();

                    // Generate output string
                    string imageName = Path.GetFileNameWithoutExtension(imagePath);
                    string outputText = $"{imageName}, {string.Join(", ", results)}";

                    // Save processed image
                    string outputImagePath = Path.Combine(outputFolder, $"{imageName}_processed.jpg");
                    inputImage.Save(outputImagePath);

                    // Save results to text file
                    string outputTextPath = Path.Combine(outputFolder, $"{imageName}_results.txt");
                    File.WriteAllText(outputTextPath, outputText);

                    // Display the processed image in pictureBox2
                //    pictureBox2.Image = inputImage.ToBitmap();
                    // Load the processed image back into the PictureBox
                    pictureBox2.Image = Image.FromFile(outputTextPath);

                    // Show results
                    MessageBox.Show($"Results:\n{outputText}\n\nProcessed image saved to: {outputImagePath}",
                        "Processing Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}