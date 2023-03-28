using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CREATE_AREA_FILTERS
{
    public partial class TreeViewExample : Form
    {
        public TreeViewExample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server;
            string sql;

            server = 'br-ppm-62';


            //--------- //PRIMEIRO NODE---------------------

            sql = "select N1.itemname as AREA_N1, N1.oid from ##JConfigProjectRoot PR join ##XSystemHierarchy x1 on x1.oidorigin=PR.oid Join ##jnameditem N1 on N1.oid=x1.OidDestination";
            sql = sql.Replace("##", "[" + cproj.Text + "_RDB].dbo.");
            string connectionString = "Data Source =" + server + "; Initial Catalog = master; Integrated Security = SSPI; Trusted_Connection=True;";
            SqlConnection sqlConn = new SqlConnection(connectionString);

            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();

            //LIMPAR O TREEVIEW
            TV.Nodes.Clear();
            //ADICIONAR PRIMEIRO NODE
            TV.Nodes.Add(cproj.Text);
            var node = TV.Nodes[0];

            while (dr.Read())
            {
                string AREA1 = "";
                string OID1 = "";
                AREA1 = (string)dr[0];
                OID1 = dr[1].ToString();


                //ADICIONAR INFORMAÇÃO NO NODE (KEY, VALUE)
                node.Nodes.Add(OID1, AREA1);

                //SEGUNDO NODE PARA CADA NODE1
                string sql2;
                SqlConnection sqlConn2 = new SqlConnection(connectionString);
                sql2 = "SELECT N2.itemname as AREA_N2 , N2.oid FROM ##JAreaSystem A JOIN ##XSystemHierarchy x2 on x2.oidorigin = A.oid Join ##jnameditem N1 on N1.oid = x2.oidorigin ";
                sql2 = sql2 + " JOIN ##JNamedItem N2 on N2.oid = x2.OidDestination JOIN ##JAreaSystem A2 on A2.oid=N2.oid WHERE N1.OID ='" + OID1 + "'";
                sql2 = sql2.Replace("##", "[" + cproj.Text + "_RDB].dbo.");
                sqlConn2.Open();
                SqlCommand cmd2 = new SqlCommand(sql2, sqlConn2);
                SqlDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    string AREA2 = "";
                    string OID2 = "";
                    AREA2 = (string)dr2[0];
                    OID2 = dr2[1].ToString();

                    //ENTRAR NO NODE OID1 E ADICIONAR UM NODE FILHO NELE
                    node.Nodes[OID1].Nodes.Add(OID2, AREA2);

                    //TERCEIRO NODE PARA CADA NODE2
                    string sql3;
                    sql3 = "SELECT N2.itemname as AREA_N2 , N2.oid FROM ##JAreaSystem A JOIN ##XSystemHierarchy x2 on x2.oidorigin = A.oid Join ##jnameditem N1 on N1.oid = x2.oidorigin ";
                    sql3 = sql3 + " JOIN ##JNamedItem N2 on N2.oid = x2.OidDestination JOIN ##JAreaSystem A2 on A2.oid=N2.oid WHERE N1.OID ='" + OID2 + "'";
                    sql3 = sql3.Replace("##", "[" + cproj.Text + "_RDB].dbo.");
                    SqlConnection sqlConn3 = new SqlConnection(connectionString);
                    sqlConn3.Open();
                    SqlCommand cmd3 = new SqlCommand(sql3, sqlConn3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();

                    while (dr3.Read())
                    {
                        string AREA3 = "";
                        string OID3 = "";
                        AREA3 = (string)dr3[0];
                        OID3 = dr3[1].ToString();

                        //ABREVIAR O NOME DO NODE PARA NODE2 E ADICIONAR UM NETO
                        var node2 = node.Nodes[OID1].Nodes[OID2];
                        node2.Nodes.Add(OID3, AREA3);
                    }
                    sqlConn3.Close();
                }
                sqlConn2.Close();
            }
            sqlConn.Close();
        }
    }
}
