using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GraphShortest
{
    public partial class MnFrm : Form
    {
        public MnFrm()
        {
            InitializeComponent();
        }

        int[,] Rtable;
        int[,] Qtable;
        Point[] dugumler;
        int sayac = 0;

        bool mouseDOWN = false;
        bool mouseUP = true;
        int foundINDEX = -1;

        int baslangic = -1;
        int bitis = -1;

        public void BubbleSort(int[] dizi)
        {
            int gecici;

            for (int i = 0; i <= dizi.Length - 1; i++)
            {
                for (int j = 1; j <= dizi.Length - 1; j++)
                {
                    if (dizi[j - 1] > dizi[j])
                    {
                        gecici = dizi[j - 1];
                        dizi[j - 1] = dizi[j];
                        dizi[j] = gecici;
                    }
                }
            }
        }

        public void BubbleSortIndex(int[] dizi,int[] index)
        {
            int gecici;
            int temp;

            for (int i = 0; i <= dizi.Length - 1; i++)
            {
                for (int j = 1; j <= dizi.Length - 1; j++)
                {
                    if (dizi[j - 1] > dizi[j])
                    {
                        gecici = dizi[j - 1];
                        temp = index[j - 1];
                        dizi[j - 1] = dizi[j];
                        index[j - 1] = index[j];
                        dizi[j] = gecici;
                        index[j] = temp;


                    }
                }
            }
        }

        public void DrawMatris(int[,] diziS, Panel panelS)
        {
            if (diziS == null)
                return;

            panelS.Refresh();

            SolidBrush br = new SolidBrush(SystemColors.ControlText);
     
            Graphics g = panelS.CreateGraphics();

            int diziUzunluk=(int)Math.Sqrt(diziS.Length);

            for (int i = 0; i <diziUzunluk; i++){
                for (int j = 0; j <diziUzunluk; j++){
                 g.DrawString(diziS[i,j].ToString(), this.Font, br,i*20+30,j*20+30);
                }
            }

            for (int i = 0; i < listDugumler.Items.Count; i++){
                g.DrawString(listDugumler.Items[i].ToString(), this.Font, br,i*20+30,15);
            }

            for (int i = 0; i < listDugumler.Items.Count; i++){
                g.DrawString(listDugumler.Items[i].ToString(), this.Font, br,15,i*20+30);
            }

        }

        public void DrawGraphNode(int[,] diziS, Panel panelS)
        {
            if (diziS == null)
                return;

            panelS.Refresh();

            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);

            Graphics g = panelS.CreateGraphics();

            int diziUzunluk = (int)Math.Sqrt(diziS.Length);

            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < diziUzunluk; i++){
                
                Point p = new Point();

                p.X = rand.Next(300);
                p.Y = rand.Next(300);

                dugumler[i] = p;

                g.DrawString(listDugumler.Items[i].ToString(), this.Font, br, p);
                g.DrawEllipse(Pens.Blue, p.X-3, p.Y-3, 20, 20);

            }
         
        }

        public void DrawGraphNodeMouse(int[,] diziS, Panel panelS)
        {
            if (diziS == null)
                return;

            panelS.Refresh();

            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);

            Graphics g = panelS.CreateGraphics();

            int diziUzunluk = (int)Math.Sqrt(diziS.Length);

            for (int i = 0; i < diziUzunluk; i++){

                g.DrawString(listDugumler.Items[i].ToString(), this.Font, br,dugumler[i]);
                g.DrawEllipse(Pens.Blue, dugumler[i].X - 3, dugumler[i].Y - 3, 20, 20);

            }

        }

        public void DrawGraphLines(int[,] diziS,Point[] dugumler, Panel panelS,ListBox kisayol)
        {
            if (diziS == null)
                return;

            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);

            Graphics g = panelS.CreateGraphics();

            int diziUzunluk = (int)Math.Sqrt(diziS.Length);

            StreamWriter yazici = new StreamWriter("koord.txt");

            string koordinatInfo = null;

            for (int i = 0; i < diziUzunluk; i++)
            {
                for (int j = 0; j < diziUzunluk; j++)
                {

                    if (diziS[i, j] == 0)
                    {
                            g.DrawLine(Pens.Black, dugumler[i], dugumler[j]);

                            koordinatInfo = koordinatInfo + "0." + dugumler[i].X.ToString() + ","+"0."+dugumler[i].Y.ToString()+",0.0,";
                            koordinatInfo = koordinatInfo + "0." + dugumler[j].X.ToString() + "," + "0." + dugumler[j].Y.ToString() + ",0.0,";
                            koordinatInfo = koordinatInfo + "1,1,1";
                            
                            yazici.WriteLine(koordinatInfo);

                            koordinatInfo = null;
                    }

                }
            }

            if (kisayol != null)
            {
                for (int i = 0; i < kisayol.Items.Count-1; i++)
                {
                    g.DrawLine(Pens.Red,
                        dugumler[listDugumler.Items.IndexOf(kisayol.Items[i])].X,
                        dugumler[listDugumler.Items.IndexOf(kisayol.Items[i])].Y,
                        dugumler[listDugumler.Items.IndexOf(kisayol.Items[i + 1])].X,
                        dugumler[listDugumler.Items.IndexOf(kisayol.Items[i+1])].Y);

                    koordinatInfo = koordinatInfo + "0." + dugumler[listDugumler.Items.IndexOf(kisayol.Items[i])].X.ToString() + "," + "0." + dugumler[listDugumler.Items.IndexOf(kisayol.Items[i])].Y.ToString() + ",0.03,";
                    koordinatInfo = koordinatInfo + "0." + dugumler[listDugumler.Items.IndexOf(kisayol.Items[i + 1])].X.ToString() + "," + "0." + dugumler[listDugumler.Items.IndexOf(kisayol.Items[i+1])].Y.ToString() + ",0.03,";
                    koordinatInfo = koordinatInfo + "1,1,0";

                    yazici.WriteLine(koordinatInfo);

                    koordinatInfo = null;

                }
            }

            yazici.Close();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtDugumAd.Text == "")
                return;

            if (listDugumler.Items.IndexOf(txtDugumAd.Text) != -1) {
                MessageBox.Show("Ayný kayýttan var", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          
            listDugumler.Items.Add(txtDugumAd.Text);
            comboBas.Items.Add(txtDugumAd.Text);
            comboBitis.Items.Add(txtDugumAd.Text);
            comboBoxNode1.Items.Add(txtDugumAd.Text);
            comboBoxNode2.Items.Add(txtDugumAd.Text);
            txtDugumAd.ResetText();
            
            int count = listDugumler.Items.Count;

            Rtable = new int[count, count];
            Qtable = new int[count, count];
            dugumler = new Point[count];

            for (int i = 0; i < count; i++){
                for (int j = 0; j < count; j++){
                    Rtable[i, j] = -1;
                }
            }

            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNode(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph,listBox1);

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listDugumler.SelectedItem ==null)
                return;

            comboBas.Items.Remove(listDugumler.SelectedItem.ToString());
            comboBitis.Items.Remove(listDugumler.SelectedItem.ToString());
            comboBoxNode1.Items.Remove(txtDugumAd.Text.ToString());
            comboBoxNode2.Items.Remove(txtDugumAd.Text.ToString());
            listDugumler.Items.Remove(listDugumler.SelectedItem.ToString());

            int count = listDugumler.Items.Count;

            Rtable = new int[count, count];
            Qtable = new int[count, count];
            dugumler = new Point[count];

            for (int i = 0; i < count; i++){
                for (int j = 0; j < count; j++){
                    Rtable[i, j] = -1;
                }
            }

            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNode(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);

            
        }

        private void btnYolEkle_Click(object sender, EventArgs e)
        {

            if (comboBoxNode1.Text == comboBoxNode2.Text)
            {
                MessageBox.Show("Ayný düðüme yol veremezsiniz !", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string yol = null;

            yol = comboBoxNode1.SelectedItem.ToString() + " --> ";
            yol = yol+comboBoxNode2.SelectedItem.ToString();

            if (listBaglar.Items.IndexOf(yol) != -1){
                MessageBox.Show("Ayný kayýttan var", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

                listBaglar.Items.Add(yol);

            int node1 = comboBoxNode1.SelectedIndex;
            int node2 = comboBoxNode2.SelectedIndex;

            Rtable[node1, node2] = 0;

            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNodeMouse(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);


            listBaglar.Sorted = true;

        }



        private void btnYolSil_Click(object sender, EventArgs e)
        {
            if (listBaglar.SelectedItem == null)
                return;

            int index1 = 0;
            int index2 = 0;


            string[] dugumInfo = listBaglar.SelectedItem.ToString().Split(' ');

            index1 = listDugumler.Items.IndexOf(dugumInfo[0]);
            index2 = listDugumler.Items.IndexOf(dugumInfo[2]);


            listBaglar.Items.Remove(listBaglar.SelectedItem);

            Rtable[index1, index2] = -1;

            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNodeMouse(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);

            listBaglar.Sorted = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNode(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (comboBas.Text == "" || comboBitis.Text == "")
            {
                MessageBox.Show("Önce düðümleri seçiniz");
                return;
            }

            baslangic = comboBas.SelectedIndex;
            bitis = comboBitis.SelectedIndex;

            int sayi = listDugumler.Items.Count;


            for (int i = 0; i < sayi; i++){
                for (int j = 0; j < sayi; j++){
                    if (Rtable[i, j] != -1){
                        Rtable[i,j] = 0;
                    }
                    Qtable[i, j] = 0;
                }

            }

            for (int i = 0;i<sayi; i++){
                if (Rtable[i,bitis] == 0){
                    Rtable[i,bitis] = 100;
                }
            }

            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNode(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);

        }

        public int MaximumBul(int m)
        {
            int[] tempDizi = new int[listDugumler.Items.Count];

             for (int i = 0; i < listDugumler.Items.Count; i++){
                tempDizi[i] = Qtable[m,i];
            }

            BubbleSort(tempDizi);

            return tempDizi[listDugumler.Items.Count - 1];
 
        }

        private void btniter_Click(object sender, EventArgs e)
        {
            double Alpha = 0.8;

            if (baslangic == -1 || bitis == -1){
                MessageBox.Show("Önce baþlangýç ve bitiþ noktalarýný belirleyiniz");
                return;
            }
         
            for (int k = 0; k < listDugumler.Items.Count; k++){
                for (int i = 0; i < listDugumler.Items.Count; i++){
                    for (int j = 0; j < listDugumler.Items.Count; j++){
                        if (Rtable[i, j] != -1){
                            int maxValue = MaximumBul(j);
                            Qtable[i, j] = Rtable[i, j] + (int)(Alpha * maxValue);
                        }
                    }
                }
            }

            YolBul();

            DrawMatris(Rtable, panelRtable);
            DrawMatris(Qtable, panelQtable);
            DrawGraphNode(Rtable, panelGraph);
            DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);
        
    }

     
        private void YolBul()
        {
            int durum = -1;
            int oncekiDurum = -1;

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            durum = baslangic;


            int sayi = listDugumler.Items.Count;

            listBox1.Items.Add(listDugumler.Items[durum].ToString());


            while (durum != bitis){
                int[] dizi = new int[sayi];
                int[] index = new int[sayi];
                for (int i = 0; i < sayi; i++){
                    index[i] = i;
                    dizi[i] = Qtable[durum, i];
                }

                BubbleSortIndex(dizi, index);

                oncekiDurum = durum;
                durum = index[sayi - 1];

                listBox2.Items.Add(listDugumler.Items[oncekiDurum].ToString() + "-->" + listDugumler.Items[durum].ToString());
                listBox1.Items.Add(listDugumler.Items[durum].ToString());
            }
        }

        private void btn3D_Click(object sender, EventArgs e)
        {
            StreamWriter yazici = new StreamWriter(Application.StartupPath + "\\app\\decode.bat");

            string[] metin;
            char a = '"';

            string ayrac = null;

            ayrac = a.ToString();

            metin = new string[4];

            metin[0] = "color ba";
            metin[1] = "java -jar "+ ayrac + Application.StartupPath + "\\app\\GraphViewer.jar"+ayrac;
            metin[2] = "del " + ayrac + Application.StartupPath + "\\app\\decode.bat" + ayrac;
            metin[3] = "exit";

            for (int i = 0; i <= 2; i++){
                yazici.WriteLine(metin[i]);
            }

            yazici.Close();

            //////////////////////////////////////////////////////////////////////

            Process decode_bat = new Process();

            decode_bat.StartInfo.FileName = Application.StartupPath + "\\app\\decode.bat";
            decode_bat.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            decode_bat.Start();
            //decode_bat.WaitForExit();
        }

      
    
        private void panelGraph_MouseMove(object sender, MouseEventArgs e)
        {
            int offset = 20;
            
            if (mouseDOWN == true){
                for (int i = 0; i < dugumler.Length; i++){
                    if (dugumler[i].X < e.X + offset && dugumler[i].X > e.X - offset && 
                        dugumler[i].Y < e.Y + offset && dugumler[i].Y > e.Y - offset){
                        if (i == foundINDEX){
                            dugumler[i].X = e.X;
                            dugumler[i].Y = e.Y;
                        } 
                    }
                }

                DrawGraphNodeMouse(Rtable, panelGraph);
                DrawGraphLines(Rtable, dugumler, panelGraph, listBox1);

            }
        }

        private void panelGraph_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDOWN = true;

            int offset = 20;

            if (foundINDEX == -1)
            {
                for (int i = 0; i < dugumler.Length; i++)
                {
                    if ((dugumler[i].X < e.X + offset && dugumler[i].X > e.X - offset) ||
                        (dugumler[i].Y < e.Y + offset && dugumler[i].Y > e.Y - offset))
                    {
                        dugumler[i].X = e.X;
                        dugumler[i].Y = e.Y;
                        foundINDEX = i;
                    }
                }
            }
        }

        private void panelGraph_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDOWN = false;
            foundINDEX = -1;
        }

        private void MnFrm_Load(object sender, EventArgs e)
        {
            //this.Visible = false;
            //this.SetDesktopLocation(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
            //this.Height = 0;
            //this.Width = 0;
            //this.Visible = true;
            //timerEkran.Enabled = true;

            Random rnd = new Random();

            string[] nodes = { "A", "B", "C", "D", "E"};

            for (int i = 0; i < nodes.Length; i++){
                this.txtDugumAd.Text = nodes[i];
                this.btnEkle.PerformClick();
            }

            for (int i = 0; i < nodes.Length-1; i++) { 
                this.comboBoxNode1.SelectedIndex = i;
                this.comboBoxNode2.SelectedIndex = i+1;
                this.btnYolEkle.PerformClick();
            }


            this.comboBoxNode1.SelectedIndex = 0;
            this.comboBoxNode2.SelectedIndex = 2;
            this.btnYolEkle.PerformClick();

            this.comboBas.SelectedIndex = 0;
            this.comboBitis.SelectedIndex = 4;
            this.btnTamam.PerformClick();

            this.btniter.PerformClick();


        }

        private void timerEkran_Tick(object sender, EventArgs e)
        {
            sayac++;

            if (sayac == 700){
                timerEkran.Enabled = false;
                return;
            }
            
            int xx = Screen.PrimaryScreen.Bounds.Width / 2 - sayac;
            int yy = Screen.PrimaryScreen.Bounds.Height / 2 - sayac;
            
            if (xx <= 0)
                xx = 0;
            if (yy <= 0)
                yy = 0;

            this.SetDesktopLocation(xx,yy);
            this.Height = sayac-150;
            this.Width = sayac+400;


        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}