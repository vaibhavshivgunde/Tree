using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication1
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                PopulateRootLevel();
            }
        }
        private void PopulateRootLevel()
        {
            String CS = ConfigurationManager.ConnectionStrings["DBCON"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select id, title,URL, (select count(*) FROM SampleCategories WHERE parentid=sc.id) childnodecount FROM SampleCategories sc where parentID IS NULL", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                PopulateNodes(dt, TreeView1.Nodes);
            }
        }
        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr["title"].ToString();
                tn.Value = dr["id"].ToString();
                tn.NavigateUrl = dr["URL"].ToString();
                nodes.Add(tn);
                tn.PopulateOnDemand = (Convert.ToInt32(dr["childnodecount"]) > 0);
            }
        }
        protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        }
        private void PopulateSubLevel(int parentID, TreeNode parentNode)
        {
            String con1 = ConfigurationManager.ConnectionStrings["DBCON"].ConnectionString;
            String CS = ConfigurationManager.ConnectionStrings["DBCON"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select id,title,URL,(select count(*) FROM SampleCategories WHERE parentid=sc.id) childnodecount FROM SampleCategories sc where parentID=@parentID", con);
                cmd.Parameters.Add("@parentID", parentID);
                //con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                PopulateNodes(dt, parentNode.ChildNodes);
            }
        }
    }
}