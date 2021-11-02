using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Model_Accesse.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace DAL
{
    public class ReportGeneration
    {
        private string Filepath = Path.Combine(Environment.CurrentDirectory, "Test.pdf");
        private Color background = WebColors.GetRGBColor("#D3D3D3");

        public ReportGeneration()
        {
        }

        public void WriteReport(ObservableCollection<TourLog> TourLogs, Tour tour)
        {
            PdfWriter writer = new PdfWriter(Filepath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Tour Report")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(header);

            if (tour.routeInfo != "https://tenomad.com/wp-content/themes/unbound/images/No-Image-Found-400x264.png")
            {
                Paragraph subheader = new Paragraph(tour.Name).SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
                document.Add(subheader);
                Uri imageUrl = new Uri(tour.routeInfo, UriKind.Absolute);
                ImageData imageData = ImageDataFactory.CreateJpeg(imageUrl);
                var image = new Image(imageData);
                image.SetAutoScale(true);
                document.Add(new Paragraph().Add(image));
                //Display the image
                //Maybe something else
            }
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            Table table = new Table(9, false);
            Cell cell11 = new Cell(1, 1)
                   .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Date"));
            Cell cell12 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Start Time:"));
            Cell cell13 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("End Time:"));
            Cell cell14 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Total Time"));
            Cell cell15 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Distance"));
            Cell cell16 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Average Speed"));
            Cell cell17 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Calories Burned"));
            Cell cell18 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Rating:"));
            Cell cell19 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Comment"));
            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell14);
            table.AddCell(cell15);
            table.AddCell(cell16);
            table.AddCell(cell17);
            table.AddCell(cell18);
            table.AddCell(cell19);
            int oTotalTime = 0;
            int oDistance = 0;
            double oBurnedCalories = 0;
            double oAvgSpeed = 0;
            double oAvgRating = 0;
            foreach (TourLog log in TourLogs)
            {
                Cell cell21 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.date));
                Cell cell22 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.startTime));
                Cell cell23 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.endTime));
                Cell cell24 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.totalTime));
                Cell cell25 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.distance));
                Cell cell26 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.avgSpeed));
                Cell cell27 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.burnedCalories));
                Cell cell28 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.rating));
                Cell cell29 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph(log.comment));
                oTotalTime += Int32.Parse(log.totalTime);
                oDistance += Int32.Parse(log.distance);
                oBurnedCalories += Double.Parse(log.burnedCalories.Replace(".", ","));
                oAvgSpeed += Double.Parse(log.avgSpeed.Replace(".", ","));
                oAvgRating += Double.Parse(log.rating.Replace(".", ","));
                table.AddCell(cell21);
                table.AddCell(cell22);
                table.AddCell(cell23);
                table.AddCell(cell24);
                table.AddCell(cell25);
                table.AddCell(cell26);
                table.AddCell(cell27);
                table.AddCell(cell28);
                table.AddCell(cell29);
            }
            Cell cell31 = new Cell(1, 1)
                   .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("---"));
            Cell cell32 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("---"));
            Cell cell33 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("---"));
            Cell cell34 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Overall Total Time:"));
            Cell cell35 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Overall Distance"));
            Cell cell36 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Overall Average Speed"));
            Cell cell37 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Overall Calories Burned"));
            Cell cell38 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Average Rating:"));
            Cell cell39 = new Cell(1, 1)
                .SetBackgroundColor(background)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("---"));
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell33);
            table.AddCell(cell34);
            table.AddCell(cell35);
            table.AddCell(cell36);
            table.AddCell(cell37);
            table.AddCell(cell38);
            table.AddCell(cell39);
            Cell cell41 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("---"));
            Cell cell42 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("---"));
            Cell cell43 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("---"));
            Cell cell44 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(oTotalTime.ToString()));
            Cell cell45 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(oDistance.ToString()));
            Cell cell46 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("---"));
            Cell cell47 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(oBurnedCalories.ToString()));
            Cell cell48 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("---"));
            Cell cell49 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("---"));

            table.AddCell(cell41);
            table.AddCell(cell42);
            table.AddCell(cell43);
            table.AddCell(cell44);
            table.AddCell(cell45);
            table.AddCell(cell46);
            table.AddCell(cell47);
            table.AddCell(cell48);
            table.AddCell(cell49);
            document.Add(table);

            document.Close();
        }
    }
}
