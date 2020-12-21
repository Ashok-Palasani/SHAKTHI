using Dapper;
using SRKSFanucDAL.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRKSFanucDAL.DAO
{
    public class Dao : ConnectionFactory
    {
        IConnectionFactory _connectionFactory;
        string databaseName = "";
        public Dao()
        {

        }
        public Dao(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            databaseName = ConfigurationManager.AppSettings["databasename"];
        }

        public int Axisdet1_multipeldatainsert(List<AxisDetails1> AxisDetlist1)
        {
            int res = 0;
            try
            {
                Repository<AxisDetails1> lista = new Repository<AxisDetails1>();
                string query = "INSERT INTO  tbl_axisdetails1(MachineID,Axis,AbsPos,RelPos,MacPos,DistPos,StartTime,EndTime,IsDeleted,InsertedOn) VALUES(@MachineID,@Axis,@AbsPos,@RelPos,@MacPos,@DistPos,@StartTime,@EndTime,@IsDeleted,@InsertedOn)";
                using (var con = _connectionFactory.GetConnection)
                {
                    res = con.Execute(query, AxisDetlist1, commandTimeout: 600);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                 IntoFile(ex.ToString());
            }
            return res;
        }

        public int Axisdet2_multipeldatainsert(List<AxisDetails2> AxisDetlist2)
        {
            int res = 0;
            try
            {
                Repository<AxisDetails2> lista = new Repository<AxisDetails2>();
                string query = "INSERT INTO  tbl_axisdetails2(MachineID,StartTime,EndTime,FeedRate,SpindleLoad,SpindleSpeed,IsDeleted,InsertedOn) VALUES(@MachineID,@StartTime,@EndTime,@FeedRate,@SpindleLoad,@SpindleSpeed,@IsDeleted,@InsertedOn)";
                ConnectionFactory _connectionFactory = new ConnectionFactory();
                using (var con = _connectionFactory.GetConnection)
                {
                    res = con.Execute(query, AxisDetlist2, commandTimeout: 600);
                    con.Close();
                }
                //res = lista.Insert(query, _connectionFactory.GetConnection);
            }
            catch (Exception ex)
            {
                //IntoFile(ex.ToString());
                 IntoFile(ex.ToString());
            }
            return res;
        }

        public int servo_multipeldatainsert(List<ServoDeatailsModel> AxisDetlist2)
        {
            int res = 0;
            try
            {
                Repository<ServoDeatailsModel> lista = new Repository<ServoDeatailsModel>();
                string query = "INSERT INTO  tbl_servodetails(MachineID,StartDateTime,ServoAxis,ServoLoadMeter,LoadCurrent,LoadCurrentAmp,InsertedOn,Insertedby,IsDeleted) VALUES(@MachineID,@StartDateTime,@ServoAxis,@ServoLoadMeter,@LoadCurrent,@LoadCurrentAmp,@InsertedOn,@Insertedby,@IsDeleted)";
                using (var con = _connectionFactory.GetConnection)
                {
                    res = con.Execute(query, AxisDetlist2, commandTimeout: 600);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                 IntoFile(ex.ToString());
            }
            return res;
        }

        public int machinerealtimestatus_multipeldatainsert(List<MachineStatusModel> AxisDetlist2)
        {
            int res = 0;
            try
            {
                Repository<MachineStatusModel> lista = new Repository<MachineStatusModel>();
                string query = "INSERT INTO  tbl_machinestatusrealtime(MachineID,MachineStatus,MachineEmergency,MachineAlarm,CreatedOn,CreatedBy,IsDeleted,CorrectedDate) VALUES(@MachineID,@MachineStatus,@MachineEmergency,@MachineAlarm,@CreatedOn,@CreatedBy,@IsDeleted,@CorrectedDate)";
                using (var con = _connectionFactory.GetConnection)
                {
                    res = con.Execute(query, AxisDetlist2, commandTimeout: 600);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                 IntoFile(ex.ToString());
            }
            return res;
        }
        public void IntoFile(string Msg)
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = @"D:\SRKS\BackgroundApplication\ToolLife\ToolLifeLogger.txt";
            using (StreamWriter writer = new StreamWriter(appPath, true)) //true => Append Text
            {
                writer.WriteLine(System.DateTime.Now + ":  " + Msg);
            }
        }

    }
}
