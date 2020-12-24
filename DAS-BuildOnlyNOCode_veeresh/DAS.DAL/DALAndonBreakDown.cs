using DAS.DAL.Resource;
using DAS.DBModels;
using DAS.EntityModels;
using DAS.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

//namespace DAS.DAL
//{
//    public class DALAndonBreakDown:IAndonBreakDown
//    {
//        i_facility_tsalContext db = new i_facility_tsalContext();

//        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DALHMIDetails));

//        public DALAndonBreakDown(i_facility_tsalContext _db)
//        {
//            db = _db;
//        }

//        //On start of the Break down call this to generate the CSV file
//        public CommonResponse BreakDownStart(int machineIds)
//        {
//            CommonResponse obj = new CommonResponse();
//            try
//            {

//                //FileInfo templateFile = new FileInfo(@"C:\TataReport\NewTemplates\Andon_BreakDownStart.xlsx");
//                //ExcelPackage templatep = new ExcelPackage(templateFile);
//                //ExcelWorksheet Templatews = templatep.Workbook.Worksheets[0];
//                string correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
//                string fileNameIn = "";

//                string FileDir = db.Tbltosapfilepath.Where(x => x.IsDeleted == 0 && x.IsBreakDown == 1 && x.PathName == "Start").Select(x => x.Path).FirstOrDefault();


//                bool exists = System.IO.Directory.Exists(FileDir);

//                if (!exists)
//                {
//                    System.IO.Directory.CreateDirectory(FileDir);
//                }

//                Tblmachinedetails machineDetails1 = db.Tblmachinedetails.Where(x => x.EquipmentNum != null && x.MachineId == machineIds).FirstOrDefault();
//                if (machineDetails1 != null)
//                {
//                    int machineId = machineDetails1.MachineId;
//                    Tblbreakdown breakdownDet1 = db.Tblbreakdown.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate).OrderByDescending(x => x.BreakdownId).FirstOrDefault();
//                    if (breakdownDet1 != null)
//                    {
//                        int breakDownId = breakdownDet1.BreakdownId;
//                        DateTime startTime = Convert.ToDateTime(breakdownDet1.StartTime);
//                        fileNameIn = "bdc_" + breakDownId + "_" + startTime.ToString("yyyyMMdd") + "_" + startTime.ToString("HHmmss") + ".csv";
//                    }
//                }

//                //string fileNameIn = "BreakdownStart" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

//                FileInfo newFile = new FileInfo(System.IO.Path.Combine(FileDir, fileNameIn));
//                if (newFile.Exists)
//                {
//                    try
//                    {
//                        newFile.Delete();
//                        newFile = new FileInfo(System.IO.Path.Combine(FileDir, fileNameIn));
//                    }
//                    catch
//                    {

//                    }
//                }
//                //Using the File for generation and populating it
//                //ExcelPackage p = null;
//                //p = new ExcelPackage(newFile);
//                //ExcelWorksheet worksheet = null;

//                ////Creating the WorkSheet for populating
//                //try
//                //{
//                //    worksheet = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("dd-MM-yyyy"), Templatews);
//                //}
//                //catch { }

//                //if (worksheet == null)
//                //{
//                //    worksheet = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("dd-MM-yyyy"), Templatews);
//                //}

//                //int sheetcount = p.Workbook.Worksheets.Count;
//                //p.Workbook.Worksheets.MoveToStart(sheetcount - 1);

//                //worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
//                //worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

//               // int startRow = 2;
//                string fileName = "";
//                string Path = "";

//                //string correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
//                List<Tblmachinedetails> machineDetails = db.Tblmachinedetails.Where(x => x.EquipmentNum != null && x.MachineId == machineIds).ToList();
//                foreach (Tblmachinedetails machRow in machineDetails)
//                {
//                    int machineId = machRow.MachineId;
//                    string machineInvNo = machRow.MachineInvNo;
//                   // string equipmentNo = machRow.EquipmentNum;
//                   // string machineDisplay = machRow.MachineDispName;
//                    Tblbreakdown breakdownDet = db.Tblbreakdown.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate).OrderByDescending(x => x.BreakdownId).FirstOrDefault();
                    
//                    int breakDownId = breakdownDet.BreakdownId;
//                    DateTime startTime = Convert.ToDateTime(breakdownDet.StartTime);
//                    List<Breakdowndata> Breakdata = new List<Breakdowndata>();
//                    if (breakdownDet != null)
//                    {
//                        Breakdowndata bddata = new Breakdowndata();
                       
//                     //int breakDownId = breakdownDet.BreakdownId;
//                        //DateTime startTime = Convert.ToDateTime(breakdownDet.StartTime);
//                       // string messageDec = breakdownDet.MessageDesc;
//                        fileName = "bdc_" + breakDownId + "_" + startTime.ToString("yyyyMMdd") + "_" + startTime.ToString("HHmmss");
//                        string operatorId = Convert.ToString(db.Tbllivehmiscreen.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate && x.Date >= startTime).Select(x => x.OperatorDet).FirstOrDefault());
//                        string operatorName = db.Tbloperatordetails.Where(x => x.OperatorId == operatorId && x.IsDeleted == 0).Select(x => x.OperatorName).FirstOrDefault();
//                        //worksheet.Cells["A" + startRow].Value = startRow - 1;
//                        //worksheet.Cells["A" + startRow].Value = breakDownId;
//                        //worksheet.Cells["B" + startRow].Value = equipmentNo;
//                        //worksheet.Cells["C" + startRow].Value = machineDisplay;
//                        //worksheet.Cells["D" + startRow].Value = messageDec;
//                        bddata.breakDownId = breakDownId;
//                        bddata.equipmentNo = machRow.EquipmentNum;
//                        bddata.machineDisplay = machRow.MachineDispName; 
//                        bddata.messageDec = breakdownDet.MessageDesc;
//                        string breadkDownCodes = GetLossCode(Convert.ToInt32(breakdownDet.BreakDownCode));
//                        string[] listCodes = breadkDownCodes.Split(',');
//                        //for (int i = 0; i < listCodes.Count(); i++)
//                        //{
//                        //    if (listCodes.Count() == 3)
//                        //    {
//                        //        if (i == 0)
//                        //        {
//                        //            worksheet.Cells["J" + startRow].Value = listCodes[i];
//                        //        }
//                        //        else if (i == 1)
//                        //        {
//                        //            worksheet.Cells["I" + startRow].Value = listCodes[i];
//                        //        }
//                        //        else if (i == 2)
//                        //        {
//                        //            worksheet.Cells["H" + startRow].Value = listCodes[i];
//                        //        }
//                        //    }
//                        //    else if (listCodes.Count() == 2)
//                        //    {
//                        //        if (i == 0)
//                        //        {
//                        //            worksheet.Cells["I" + startRow].Value = listCodes[i];
//                        //        }
//                        //        else if (i == 1)
//                        //        {
//                        //            worksheet.Cells["H" + startRow].Value = listCodes[i];
//                        //        }
//                        //    }
//                        //    else if (listCodes.Count() == 1)
//                        //    {
//                        //        worksheet.Cells["H" + startRow].Value = listCodes[i];
//                        //    }

//                        //}
//                        //worksheet.Cells["E" + startRow].Value = "Yes";
//                        //worksheet.Cells["F" + startRow].Value = startTime.ToString("yyyy-MM-dd");
//                        //worksheet.Cells["G" + startRow].Value = startTime.ToString("HH:mm:ss");
//                        //worksheet.Cells["H" + startRow].Value = operatorId;
//                        //worksheet.Cells["I" + startRow].Value = operatorName;
//                        //worksheet.Cells["J" + startRow].Value = machRow.MachineInvNo;
                        
//                        bddata.Yes = "YES"; 
//                        bddata.BDstartDate = startTime.ToString("yyyy-MM-dd");
//                        bddata.BDstartTime = startTime.ToString("HH:mm:ss");
//                        bddata.operatorId = operatorId;
//                        bddata.operatorName = operatorName;
//                        bddata.MachineInvNo = machRow.MachineInvNo;
//                        Breakdata.Add(bddata);
//                        //startRow++;
//                    }
//                    DataTable BDdt = ToDataTable(Breakdata);
//                    //BDdt.Columns.Remove();
//                    //fileName = "bdc_" + breakDownId + "_" + startTime.ToString("yyyyMMdd") + "_" + startTime.ToString("HHmmss")+".csv";
//                    Path = FileDir + fileName +".csv";
//                    bool res = PushDataToCSV(BDdt, Path);
//                }

//                //p.Save();
//                string path1 = System.IO.Path.Combine(FileDir, fileName + ".csv");
//                //File.Copy(fileNameIn, FileDir);
//                //File.Copy(FileDir, fileNameIn, true);
//                obj.isTure = true;
//                obj.response = path1;
//                //System.IO.FileInfo file1 = new System.IO.FileInfo(Path);
//                //string Outgoingfile = "EQUP" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
//                //if (file1.Exists)
//                //{
//                //    Response.Clear();
//                //    Response.ClearContent();
//                //    Response.ClearHeaders();
//                //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
//                //    Response.AddHeader("Content-Length", file1.Length.ToString());
//                //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                //    Response.WriteFile(file1.FullName);
//                //    Response.Flush();
//                //    Response.Close();
//                //}


//            }
//            catch (Exception ex)
//            {
//                obj.isTure = false;
//                obj.response = ResourceResponse.ExceptionMessage;
//                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//            }
//            return obj;
//        }

//        //Get the LossCodes on LossCode id
//        public string GetLossCode(int breakdownCodeId)
//        {
//            string result = "";
//            try
//            {
//                Tbllossescodes breakdownDesc = db.Tbllossescodes.Where(x => x.LossCodeId == breakdownCodeId).FirstOrDefault();
//                if (breakdownDesc != null)
//                {
//                    if (breakdownDesc.LossCodesLevel == 3)
//                    {
//                        result = breakdownDesc.LossCode + ",";
//                        result += db.Tbllossescodes.Where(x => x.LossCodeId == breakdownDesc.LossCodesLevel2Id).Select(x => x.LossCode).FirstOrDefault() + ",";
//                        result += db.Tbllossescodes.Where(x => x.LossCodeId == breakdownDesc.LossCodesLevel1Id).Select(x => x.LossCode).FirstOrDefault();
//                    }
//                    else if (breakdownDesc.LossCodesLevel == 2)
//                    {
//                        result = breakdownDesc.LossCodeDesc + ",";
//                        result += db.Tbllossescodes.Where(x => x.LossCodeId == breakdownDesc.LossCodesLevel1Id).Select(x => x.LossCode).FirstOrDefault();
//                    }
//                    else if (breakdownDesc.LossCodesLevel == 1)
//                    {
//                        result = breakdownDesc.LossCode;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//            }
//            return result;
//        }

//        //On end of the Break down call this to generate the CSV file
//        public CommonResponse BreakDownEnd(int machineIds)
//        {
//            CommonResponse obj = new CommonResponse();
//            try
//            {
//                try
//                {

//                    //FileInfo templateFile = new FileInfo(@"C:\TataReport\NewTemplates\Andon_BreakDownEnd.xlsx");
//                    //ExcelPackage templatep = new ExcelPackage(templateFile);
//                    //ExcelWorksheet Templatews = templatep.Workbook.Worksheets[0];
//                    string correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
//                    string fileNameIn = "";

//                    String FileDir = db.Tbltosapfilepath.Where(x => x.IsDeleted == 0 && x.IsBreakDown == 1 && x.PathName == "End").Select(x => x.Path).FirstOrDefault();


//                    bool exists = System.IO.Directory.Exists(FileDir);

//                    if (!exists)
//                    {
//                        System.IO.Directory.CreateDirectory(FileDir);
//                    }

//                    FileInfo newFile = new FileInfo(System.IO.Path.Combine(FileDir, fileNameIn));
//                    if (newFile.Exists)
//                    {
//                        try
//                        {
//                            newFile.Delete();
//                            newFile = new FileInfo(System.IO.Path.Combine(FileDir, fileNameIn));
//                        }
//                        catch
//                        {

//                        }
//                    }
//                    //Using the File for generation and populating it
//                    //ExcelPackage p = null;
//                    //p = new ExcelPackage(newFile);
//                    //ExcelWorksheet worksheet = null;

//                    ////Creating the WorkSheet for populating
//                    //try
//                    //{
//                    //    worksheet = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("dd-MM-yyyy"), Templatews);
//                    //}
//                    //catch { }

//                    //if (worksheet == null)
//                    //{
//                    //    worksheet = p.Workbook.Worksheets.Add(System.DateTime.Now.ToString("dd-MM-yyyy"), Templatews);
//                    //}

//                    //int sheetcount = p.Workbook.Worksheets.Count;
//                    //p.Workbook.Worksheets.MoveToStart(sheetcount - 1);

//                    //worksheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
//                    //worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

//                    //int startRow = 2;
//                    string fileName = "";
//                    string Path = "";
//                    //string correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
//                    List<Tblmachinedetails> machineDetails = db.Tblmachinedetails.Where(x => x.EquipmentNum != null && x.MachineId == machineIds).ToList();
//                    foreach (Tblmachinedetails machRow in machineDetails)
//                    {
                        
//                        int machineId = machRow.MachineId;
//                        string machineInvNo = machRow.MachineInvNo;
//                        string equipmentNo = machRow.EquipmentNum;
//                        string machineDisplay = machRow.MachineDispName;
//                        Tblbreakdown breakdownDet = db.Tblbreakdown.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate).OrderByDescending(x => x.BreakdownId).FirstOrDefault();
//                        List<BreakEnd> Breakdata = new List<BreakEnd>();
//                        int breakDownId = breakdownDet.BreakdownId;
//                        //if(breakdownDet.EndTime == null)
//                        //{
//                        //    DateTime endTime = Convert.ToDateTime(correctedDate);
//                        //}
//                        DateTime endTime = Convert.ToDateTime(breakdownDet.EndTime);
//                        if (breakdownDet != null)
//                        {
                            
//                            BreakEnd BDEnd = new BreakEnd(); 
//                            //int breakDownId = breakdownDet.BreakdownId;
//                            //DateTime endTime = Convert.ToDateTime(breakdownDet.EndTime);
//                            string messageDec = breakdownDet.MessageDesc;
//                            fileName = "bdu_" + breakDownId + "_" + endTime.ToString("yyyyMMdd") + "_" + endTime.ToString("HHmmss");
//                            string operatorId = Convert.ToString(db.Tbllivehmiscreen.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate && x.Time <= endTime).OrderByDescending(x => x.Hmiid).Select(x => x.OperatorDet).FirstOrDefault());
//                            string operatorName = db.Tbloperatordetails.Where(x => x.OperatorId == operatorId && x.IsDeleted == 0).Select(x => x.OperatorName).FirstOrDefault();
//                            //worksheet.Cells["A" + startRow].Value = startRow - 1;
//                            //worksheet.Cells["A" + startRow].Value = breakDownId;
//                            //worksheet.Cells["B" + startRow].Value = machRow.EquipmentNum;
//                            BDEnd.breakDownId = breakDownId;
//                            BDEnd.equipmentNo = machRow.EquipmentNum;
//                            //worksheet.Cells["C" + startRow].Value = machineDisplay;
//                            //worksheet.Cells["D" + startRow].Value = messageDec;
//                            //worksheet.Cells["E" + startRow].Value = messageDec;
//                            string breadkDownCodes = GetLossCode(Convert.ToInt32(breakdownDet.BreakDownCode));
//                            string[] listCodes = breadkDownCodes.Split(',');
//                            for (int i = 0; i < listCodes.Count(); i++)
//                            {
//                                if (listCodes.Count() == 3)
//                                {
//                                    if (i == 0)
//                                    {
//                                        // worksheet.Cells["G" + startRow].Value = listCodes[i];
//                                        BDEnd.listCodes = listCodes[i];
//                                    }
//                                    else if (i == 1)
//                                    {
//                                        //worksheet.Cells["F" + startRow].Value = listCodes[i];
//                                        BDEnd.listCodes = listCodes[i];
//                                    }
//                                    else if (i == 2)
//                                    {
//                                        // worksheet.Cells["E" + startRow].Value = listCodes[i];
//                                        BDEnd.listCodes = listCodes[i];
//                                    }
//                                }
//                                else if (listCodes.Count() == 2)
//                                {
//                                    if (i == 0)
//                                    {
//                                        //worksheet.Cells["F" + startRow].Value = listCodes[i];
//                                        BDEnd.listCodes = listCodes[i];
//                                    }
//                                    else if (i == 1)
//                                    {
//                                        //worksheet.Cells["E" + startRow].Value = listCodes[i];
//                                        BDEnd.listCodes = listCodes[i];
//                                    }
//                                }
//                                else if (listCodes.Count() == 1)
//                                {
//                                    // worksheet.Cells["E" + startRow].Value = listCodes[i];
//                                    BDEnd.listCodes = listCodes[i];
//                                }


//                            }
//                            //worksheet.Cells["E" + startRow].Value = "Yes";
//                            //worksheet.Cells["C" + startRow].Value = endTime.ToString("yyyy-MM-dd");
//                            //worksheet.Cells["D" + startRow].Value = endTime.ToString("HH:mm:ss");
//                            BDEnd.BDstartDate = endTime.ToString("yyyy-MM-dd");
//                            BDEnd.BDstartTime = endTime.ToString("HH:mm:ss");
//                            //worksheet.Cells["K" + startRow].Value = operatorId;
//                            //worksheet.Cells["L" + startRow].Value = operatorName;
//                            //worksheet.Cells["M" + startRow].Value = machRow.MachineInvNo;
//                            //startRow++;
//                            Breakdata.Add(BDEnd);
//                        }
//                        DataTable BDdt = ToDataTable(Breakdata);
//                        //BDdt.Columns.Remove();
//                       // string BDpath = "bdu_" + breakDownId + "_" + endTime.ToString("yyyyMMdd") + "_" + endTime.ToString("HHmmss") + ".csv";
//                        Path = FileDir + fileName+".csv";
//                        bool res = PushDataToCSV(BDdt, Path);

//                    }

//                    //p.Save();
//                    string path1 = System.IO.Path.Combine(FileDir, fileName + ".csv");
//                    //File.Copy(fileNameIn, path1,true);
//                    obj.isTure = true;
//                    obj.response = path1;
//                    //System.IO.FileInfo file1 = new System.IO.FileInfo(path1);
//                    //string Outgoingfile = "EQUP" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
//                    //if (file1.Exists)
//                    //{
//                    //    Response.Clear();
//                    //    Response.ClearContent();
//                    //    Response.ClearHeaders();
//                    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
//                    //    Response.AddHeader("Content-Length", file1.Length.ToString());
//                    //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                    //    Response.WriteFile(file1.FullName);
//                    //    Response.Flush();
//                    //    Response.Close();
//                    //}


//                }
//                catch (Exception ex)
//                {
//                    obj.isTure = false;
//                    obj.response = ResourceResponse.ExceptionMessage;
//                    log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//                }
//            }
//            catch (Exception ex)
//            {
//                obj.isTure = false;
//                obj.response = ResourceResponse.ExceptionMessage;
//                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//            }
//            return obj;
//        }

//        //On start of the Break down call this to generate the Txt file
//        public CommonResponse BreakDownStartTxtFile(int machineIds)
//        {
//            CommonResponse obj = new CommonResponse();
//            try
//            {
//                String FileDir = db.Tbltosapfilepath.Where(x => x.IsDeleted == 0 && x.IsBreakDown == 1 && x.PathName == "Start").Select(x => x.Path).FirstOrDefault();

//                string fileNameIn = "SAP_ANDON" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

//                // Check if file already exists. If yes, delete it.     
//                if (File.Exists(fileNameIn))
//                {
//                    File.Delete(fileNameIn);
//                }

//                int startRow = 2;
//                string fileName = "";

//                string correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
//                var machineDetails = db.Tblmachinedetails.Where(x => x.EquipmentNum != null && x.MachineId == machineIds).ToList();
//                foreach (var machRow in machineDetails)
//                {
//                    int machineId = machRow.MachineId;
//                    string machineInvNo = machRow.MachineInvNo;
//                    string equipmentNo = machRow.EquipmentNum;
//                    string machineDisplay = machRow.MachineDispName;
//                    var breakdownDet = db.Tblbreakdown.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate && x.DoneWithRow==0).OrderByDescending(x => x.BreakdownId).FirstOrDefault();
//                    if (breakdownDet != null)
//                    {
//                        //int breakDownId = breakdownDet.BreakdownId;
//                        string breakDownId = GetCaseIDS(breakdownDet.BreakdownId.ToString());
//                        DateTime startTime = Convert.ToDateTime(breakdownDet.StartTime);
//                        string messageDec = breakdownDet.MessageDesc;
//                        string allLevel = "";
//                        fileName = "bdc_" + breakDownId + "_" + startTime.ToString("yyyy-MM-dd").Replace("-","") + "_" + startTime.ToString("HH:mm:ss").Replace(":","");
//                        string operatorId = Convert.ToString(db.Tblhmiscreen.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate && x.Date >= startTime).Select(x => x.OperatorDet).FirstOrDefault());
//                        string operatorName = "";
//                        if (operatorId != null)
//                        {
//                            operatorName = db.Tbloperatordetails.Where(x => x.OperatorId == operatorId && x.IsDeleted == 0).Select(x => x.OperatorName).FirstOrDefault();
//                        }
//                        string breadkDownCodes = GetLossCode(Convert.ToInt32(breakdownDet.BreakDownCode));
//                        string[] listCodes = breadkDownCodes.Split(',');

//                        if(listCodes.Count()==3)
//                        {
//                            allLevel =listCodes[0]+"\t" + listCodes[1]+"\t" + listCodes[2];
//                        }
//                        else if (listCodes.Count() == 2)
//                        {
//                            allLevel = listCodes[0] + "\t " + listCodes[1]+"\t";
//                        }
//                        else if (listCodes.Count() == 1)
//                        {
//                            allLevel = listCodes[0]+"\t \t ";
//                        }

//                        //for (int i = 0; i < listCodes.Count(); i++)
//                        //{
//                        //    if (i == 0)
//                        //    {
//                        //        allLevel = listCodes[i];
//                        //    }
//                        //    else if (i == 1)
//                        //    {
//                        //        allLevel = allLevel+","+listCodes[i];
//                        //    }
//                        //    else if (i == 2)
//                        //    {
//                        //        allLevel = allLevel + "," + listCodes[i];
//                        //    }
//                        //}



//                        startRow++;

//                        fileName = fileName.Replace("-","");
//                        fileName = fileName.Replace(":", "");

//                        string finalPathWithFile = System.IO.Path.Combine(FileDir, fileName+ ".txt");
//                        //string finalPathWithFile = @"D:\SAPAndon\SAP_BreakDown_Start\"+ fileName + ".txt";

                        


//                        // Create a new file     
//                        using (System.IO.StreamWriter file =  new System.IO.StreamWriter(@finalPathWithFile))
//                        {
//                            // Add text to file    
//                            string wrireData = breakDownId + "\t" + equipmentNo + "\t" + machineDisplay
//                                 + "\t" + messageDec + "\t" + allLevel + "\t Yes" + "\t" + startTime.ToString("yyyy-mm-dd").Replace("-", ".") + "\t" +
//                                 startTime.ToString("HH:mm:ss") + "\t" + operatorId + "\t" + operatorName + "\t" + machineInvNo;


//                            file.WriteLine(wrireData);


//                            obj.isTure = true;
//                            obj.response = ResourceResponse.SuccessMessage;
//                        }
//                    }

//                }

//                //string path1 = System.IO.Path.Combine(FileDir, fileName + ".txt");
//                //File.Copy(fileNameIn, path1);
//            }
//            catch (Exception ex)
//            {
//                obj.isTure = false;
//                obj.response = ResourceResponse.ExceptionMessage;
//                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//            }
//            return obj;
//        }

//        public string GetCaseIDS(string id)
//        {
//            string result = "0";
//            if (id.Count()!=6)
//            {
//                int addZero = 6-id.Count(); 
//                for(int i=0;i< addZero; i++)
//                {
//                    if (i == 0)
//                    {
//                        result = result + id;
//                    }
//                    else
//                    {
//                        result = "0" + result;
//                    }
//                }
//            }
//            return result;
//        }

//        //On end of the Break down call this to generate the Txt file
//        public CommonResponse BreakDownEndTxtFile(int machineIds)
//        {
//            CommonResponse obj = new CommonResponse();
//            try
//            {
//                try
//                {

//                    String FileDir = db.Tbltosapfilepath.Where(x => x.IsDeleted == 0 && x.IsBreakDown == 1 && x.PathName == "End").Select(x => x.Path).FirstOrDefault();

//                    string fileNameIn = "SAP_ANDON" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

//                    // Check if file already exists. If yes, delete it.     
//                    if (File.Exists(fileNameIn))
//                    {
//                        File.Delete(fileNameIn);
//                    }

//                    int startRow = 2;
//                    string fileName = "";

//                    string correctedDate = DateTime.Now.ToString("yyyy-MM-dd");
//                    var machineDetails = db.Tblmachinedetails.Where(x => x.EquipmentNum != null && x.MachineId == machineIds).ToList();
//                    foreach (var machRow in machineDetails)
//                    {
//                        int machineId = machRow.MachineId;
//                        string machineInvNo = machRow.MachineInvNo;
//                        string equipmentNo = machRow.EquipmentNum;
//                        string machineDisplay = machRow.MachineDispName;
//                        string allLevel = "";
//                        var breakdownDet = db.Tblbreakdown.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate && x.DoneWithRow==1).OrderByDescending(x => x.BreakdownId).FirstOrDefault();
//                        if (breakdownDet != null)
//                        {
//                            //int breakDownId = breakdownDet.BreakdownId;
//                            string breakDownId = GetCaseIDS(breakdownDet.BreakdownId.ToString());
//                            DateTime endTime = Convert.ToDateTime(breakdownDet.EndTime);
//                            string messageDec = breakdownDet.MessageDesc;
//                            fileName = "bdu_"+ breakDownId + "_" + endTime.ToString("yyyy-MM-dd").Replace("-","") + "_" + endTime.ToString("HH:mm:ss").Replace(":","");
//                            string operatorId = Convert.ToString(db.Tblhmiscreen.Where(x => x.MachineId == machineId && x.CorrectedDate == correctedDate && x.Time <= endTime).OrderByDescending(x => x.Hmiid).Select(x => x.OperatorDet).FirstOrDefault());
//                            string operatorName = "";
//                            if (operatorId != null)
//                            {
//                                operatorName = db.Tbloperatordetails.Where(x => x.OperatorId == operatorId && x.IsDeleted == 0).Select(x => x.OperatorName).FirstOrDefault();
//                            }
//                            //worksheet.Cells["A" + startRow].Value = startRow - 1;
//                            //worksheet.Cells["B" + startRow].Value = breakDownId;
//                            //worksheet.Cells["C" + startRow].Value = machineInvNo;
//                            //worksheet.Cells["D" + startRow].Value = equipmentNo;
//                            //worksheet.Cells["E" + startRow].Value = machineDisplay;
//                            //worksheet.Cells["F" + startRow].Value = messageDec;
//                            string breadkDownCodes = GetLossCode(Convert.ToInt32(breakdownDet.BreakDownCode));
//                            string[] listCodes = breadkDownCodes.Split(',');

//                            if (listCodes.Count() == 3)
//                            {
//                                allLevel = listCodes[0] + "\t" + listCodes[1] + "\t" + listCodes[2];
//                            }
//                            else if (listCodes.Count() == 2)
//                            {
//                                allLevel = listCodes[0] + "\t " + listCodes[1] + "\t";
//                            }
//                            else if (listCodes.Count() == 1)
//                            {
//                                allLevel = listCodes[0] + "\t \t ";
//                            }

//                            //for (int i = 0; i < listCodes.Count(); i++)
//                            //{
//                            //    if (i == 0)
//                            //    {
//                            //        allLevel = listCodes[i];
//                            //    }
//                            //    else if (i == 1)
//                            //    {
//                            //        allLevel = allLevel + "," + listCodes[i];
//                            //    }
//                            //    else if (i == 2)
//                            //    {
//                            //        allLevel = allLevel + "," + listCodes[i];
//                            //    }
//                            //}
//                            //worksheet.Cells["J" + startRow].Value = "Yes";
//                            //worksheet.Cells["K" + startRow].Value = endTime.ToString("yyyy-mm-dd");
//                            //worksheet.Cells["L" + startRow].Value = endTime.ToString("HH:mm:ss");
//                            //worksheet.Cells["M" + startRow].Value = operatorId;
//                            //worksheet.Cells["N" + startRow].Value = operatorName;

//                            startRow++;

//                            fileName = fileName.Replace("-", "");
//                            fileName = fileName.Replace(":", "");


//                            string finalPathWithFile = System.IO.Path.Combine(FileDir, fileName + ".txt");

//                            // Create a new file     
//                            using (System.IO.StreamWriter file =  new System.IO.StreamWriter(@finalPathWithFile))
//                            {
//                                // Add text to file    
//                                string wrireData = breakDownId + "\t" + equipmentNo + "\t" + machineDisplay
//                                     + "\t" + messageDec + "\t" + allLevel + "\t Yes" + "\t" + endTime.ToString("yyyy-mm-dd").Replace("-", ".") + "\t" +
//                                     endTime.ToString("HH:mm:ss") + "\t" + operatorId + "\t" + operatorName +"\t"+ machineInvNo;


//                                file.WriteLine(wrireData);

//                                obj.isTure = true;
//                                obj.response = ResourceResponse.SuccessMessage;
//                            }
//                        }

//                    }

                    

                    
//                    //string path1 = System.IO.Path.Combine(FileDir, fileName + ".txt");
//                    //File.Copy(fileNameIn, path1);
//                    //System.IO.FileInfo file1 = new System.IO.FileInfo(path1);
//                    //string Outgoingfile = "EQUP" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
//                    //if (file1.Exists)
//                    //{
//                    //    Response.Clear();
//                    //    Response.ClearContent();
//                    //    Response.ClearHeaders();
//                    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
//                    //    Response.AddHeader("Content-Length", file1.Length.ToString());
//                    //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                    //    Response.WriteFile(file1.FullName);
//                    //    Response.Flush();
//                    //    Response.Close();
//                    //}


//                }
//                catch (Exception ex)
//                {
//                    obj.isTure = false;
//                    obj.response = ResourceResponse.ExceptionMessage;
//                    log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//                }
//            }
//            catch (Exception ex)
//            {
//                obj.isTure = false;
//                obj.response = ResourceResponse.ExceptionMessage;
//                log.Error(ex); if (ex.InnerException != null) { log.Error(ex.InnerException.ToString()); }
//            }
//            return obj;
//        }

//        //New Methods added by Ashwini

//        public DataTable ToDataTable<T>(List<T> items)
//        {
//            DataTable dataTable = new DataTable(typeof(T).Name);

//            //Get all the properties
//            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
//            foreach (PropertyInfo prop in Props)
//            {
//                //Defining type of data column gives proper data table 
//                Type type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
//                //Setting column names as Property names
//                dataTable.Columns.Add(prop.Name, type);
//            }
//            foreach (T item in items)
//            {
//                object[] values = new object[Props.Length];
//                for (int i = 0; i < Props.Length; i++)
//                {
//                    //inserting property values to datatable rows
//                    values[i] = Props[i].GetValue(item, null);
//                }
//                dataTable.Rows.Add(values);
//            }
//            //put a breakpoint here and check datatable
//            return dataTable;
//        }

//        public bool PushDataToCSV(DataTable dtDataTable, string strFilePath)
//        {
//            bool res = false;

//            StreamWriter sw = new StreamWriter(strFilePath, false);
//            //headers  
//            //for (int i = 0; i < dtDataTable.Columns.Count; i++)
//            //{
//            //    sw.Write(dtDataTable.Columns[i]);
//            //    if (i < dtDataTable.Columns.Count - 1)
//            //    {
//            //        sw.Write(",");
//            //    }
//            //}
//            //sw.Write(sw.NewLine);
//            foreach (DataRow dr in dtDataTable.Rows)
//            {
//                for (int i = 0; i < dtDataTable.Columns.Count; i++)
//                {
//                    if (!Convert.IsDBNull(dr[i]))
//                    {
//                        string value = dr[i].ToString();
//                        if (value.Contains(','))
//                        {
//                            value = String.Format("\"{0}\"", value);
//                            sw.Write(value);
//                        }
//                        else
//                        {
//                            sw.Write(dr[i].ToString());
//                        }
//                    }
//                    if (i < dtDataTable.Columns.Count - 1)
//                    {
//                        sw.Write(",");
//                    }
//                }
//                sw.Write(sw.NewLine);
//                res = true;
//            }
//            sw.Close();
//            return res;
//        }
//    }

    
//}
