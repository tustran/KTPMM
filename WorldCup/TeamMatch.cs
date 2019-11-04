using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCup
{
    public class TeamMatch //Cầu tạo 1 nhóm
    {
        Database db = new Database();        
        String req;
        SqlDataReader dr;
        private Character[] allTeam;
        public int Area;// đại diện khu vực theo thứ tự trong mô tả từ 1->7
        public int TeamID;
        public int score = 0;
        public int goal;
        public int current_onField;
        public bool disqualified = false;
        public Character[] AllTeam { get => allTeam; set => allTeam = value; }
        public object TestContext { get; private set; }
        public TeamMatch()
        {

        }
        public TeamMatch(int id, int Area)
        {
            bool check = CheckArea(Area);
            
            this.TeamID = id;
            this.Area = Area;
        }

        public bool CheckArea(int area)
        {
            //con = new System.Data.SqlClient.SqlConnection();           
            //con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename =D:\\191\\KIEMTHUPHANMEM\\ASSIGNMENT\\WORLDCUP\\WORLDCUP\\WC_DATABASE.MDF;Integrated Security=True;";
            //con.Open();
            Database db = new Database();
            db.Connect();
           
            req="Insert into Class(Nam,Id) values('aaa',4)";
            db.exeSQL(req);
            req = "SELECT * FROM dbo.Class";
            dr = db.readSQL(req);// dùng để lấy dữ liệu từ DB
            while(dr.Read())
            {
                Console.Write("Name: " + dr.GetValue(0).ToString());
                Console.Write("\t ID: "+ dr.GetValue(1).ToString());
                Console.Write("\n");
            }

            db.DisConnect();
            Console.Write("\nConnection closed");
            return true;
        }

        public Character[] registerTeam(int HLV, int TLHLV, int SSV, int CauThu) // cầu thủ <=22
        {
            TeamID = 5;
            bool qualified = checkComponent(HLV, TLHLV, SSV, CauThu);
            if (qualified == false)
            {
                return null;                
            }
            
            AllTeam = new Character[HLV + TLHLV + SSV + CauThu];
            int j = 0;
            int i;
            
            Character HLVs = new Character(1,TeamID);
            

            AllTeam[j] = HLVs;
            j++;

            for (i = 0; i < TLHLV; i++)
            {
                Character TLHLVs = new Character(2);
                AllTeam[j] = TLHLVs;
                j++;
            }

            Character SSVs = new Character(3, TeamID);
            AllTeam[j] = SSVs;
            j++;
            Console.Write("Cau thu: " + CauThu);
            for (i = 0; i < CauThu; i++)
            {
                Character CauThus = new Character(i,4, TeamID);
                AllTeam[j] = CauThus;
                j++;
            }            
            return AllTeam;
        }
       
        private Boolean checkComponent(int HLV, int TLHLV, int SSV, int CauThu)
        {
            if (HLV == 1 && (TLHLV <=3 ) && (TLHLV >= 0) && (SSV == 1) && (CauThu <= 22) && (CauThu>=0))
            {                
                return true;
            }
            else
                return false;
        }
        public int getscore(String s)
        {
            if (s.Equals("win"))
            {
                score += 3;
                return 3;
            }
            else if (s.Equals("draw"))
            {
                score += 1;
                return 1;
            }
            else if (s.Equals("lose"))
            {
                score += 0;
                return 0;
            }
            return -1;
        }
    }
}
