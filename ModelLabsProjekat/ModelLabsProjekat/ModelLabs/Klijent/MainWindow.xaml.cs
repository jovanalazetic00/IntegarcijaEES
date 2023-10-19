using FTN.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<long> comboBox1 = new List<long>();
        private List<DMSType> comboBox2 = new List<DMSType>();
        private List<long> comboBox3 = new List<long>();

        private List<ModelCode> atributi1 = new List<ModelCode>();
        private List<ModelCode> atributi2 = new List<ModelCode>();
        private List<ModelCode> atributi3 = new List<ModelCode>();

        private List<ModelCode> atributIDs = new List<ModelCode>();
        private List<ModelCode> tipovi = new List<ModelCode>();


        private long gid1;
        private DMSType modelKod2;
        private long gid3;

        private ModelCode atributID;
        private ModelCode tip;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<long> ComboBox1
        {
            get
            {
                return comboBox1;
            }
            set
            {
                comboBox1 = value;
                OnPropertyChanged("ComboBox1");
            }

        }
        public List<DMSType> ComboBox2
        {
            get
            {
                return comboBox2;
            }
            set
            {
                comboBox2 = value;
                OnPropertyChanged("ComboBox2");
            }
        }
        public List<long> ComboBox3
        {
            get
            {
                return comboBox3;
            }
            set
            {
                comboBox3 = value;
                OnPropertyChanged("ComboBox3");
            }

        }


        public List<ModelCode> Atributi1
        {
            get
            {
                if (gid1 != 0)
                {
                    return NadjiAtributeGID(gid1);
                }
                return null;
            }
            set
            {
                atributi1 = value;
                OnPropertyChanged("Atributi1");
                OnPropertyChanged("Gid1");
            }
        }
        public List<ModelCode> Atributi2
        {
            get
            {
                if (modelKod2 != 0)
                {
                    return NadjiAtributeDMS(modelKod2);
                }
                return null;
            }
            set
            {
                atributi2 = value;
                OnPropertyChanged("Atributi2");
                OnPropertyChanged("ModelKod2");
            }
        }
        public List<ModelCode> Atributi3
        {
            get
            {
                if (tip != 0)
                {
                    return NadjiAtributeMC(tip);
                }
                return null;
            }
            set
            {
                atributi3 = value;
                OnPropertyChanged("Atributi3");
            }
        }

        public long Gid1
        {
            get
            {
                return gid1;
            }
            set
            {
                gid1 = value;
                OnPropertyChanged("Gid1");
                OnPropertyChanged("Atributi1");
            }
        }
        public DMSType ModelKod2
        {
            get
            {
                return modelKod2;
            }
            set
            {
                modelKod2 = value;
                OnPropertyChanged("ModelKod2");
                OnPropertyChanged("Atributi2");
            }
        }
        public long Gid3
        {
            get
            {
                return gid3;
            }
            set
            {
                gid3 = value;
                OnPropertyChanged("Gid3");
                OnPropertyChanged("AtributIDs");
                OnPropertyChanged("Tipovi");
                OnPropertyChanged("Atributi3");
            }
        }
        public List<ModelCode> AtributIDs
        {
            get
            {
                if (gid3 != 0)
                {
                    return NadjiAtrIDs(gid3);
                }
                return null;
            }
            set
            {
                atributIDs = value;
                OnPropertyChanged("AtributIDs");
                OnPropertyChanged("Tipovi");
            }
        }
        public ModelCode AtributID
        {
            get
            {
                return atributID;
            }
            set
            {
                atributID = value;
                OnPropertyChanged("AtributID");
                NadjiTipove(atributID);
                OnPropertyChanged("Tipovi");
            }
        }

        public List<ModelCode> Tipovi
        {
            get
            {
                return tipovi;
            }
            set
            {
                tipovi = value;
                OnPropertyChanged("Tipovi");
                OnPropertyChanged("Atributi3");
            }
        }
        public ModelCode Tip
        {
            get
            {
                return tip;
            }
            set
            {
                tip = value;
                OnPropertyChanged("Tip");
                OnPropertyChanged("Atributi3");
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            Konekcija gdaProxy = new Konekcija();

            ComboBox1 = gdaProxy.GetAllGids();
            ComboBox2 = Enum.GetValues(typeof(DMSType)).Cast<DMSType>().ToList().FindAll(t => t != DMSType.MASK_TYPE);
            ComboBox3 = gdaProxy.GetAllGids();

            DataContext = this;

        }


        public static List<ModelCode> NadjiAtributeGID(long gid)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIdsForEntityId(gid);

            return lista;
        }
        public static List<ModelCode> NadjiAtributeDMS(DMSType dmstip)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIds(dmstip);

            return lista;
        }
        public static List<ModelCode> NadjiAtributeMC(ModelCode modelKod)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIds(modelKod);

            return lista;
        }
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static List<ModelCode> NadjiAtrIDs(long gid3)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            List<ModelCode> lista = modResDes.GetAllPropertyIdsForEntityId(gid3);
            List<ModelCode> rez = new List<ModelCode>();

            foreach (ModelCode mc in lista)
            {
                if (Property.GetPropertyType(mc) == PropertyType.Reference || Property.GetPropertyType(mc) == PropertyType.ReferenceVector)
                {
                    rez.Add(mc);
                }

            }

            return rez;

        }

        private List<ModelCode> NadjiTipove(ModelCode kodProp)
        {
            ModelResourcesDesc modResDes = new ModelResourcesDesc();
            //List<DMSType> tip = new List<DMSType>();

            string[] props = (kodProp.ToString()).Split('_');
            props[1] = props[1].TrimEnd('S');

            DMSType propertyCode = ModelResourcesDesc.GetTypeFromModelCode(kodProp);


            ModelCode mc;
            ModelCodeHelper.GetModelCodeFromString(propertyCode.ToString(), out mc);

            foreach (ModelCode modelCode in Enum.GetValues(typeof(ModelCode)))
            {

                if ((String.Compare(modelCode.ToString(), mc.ToString()) != 0) && (String.Compare(kodProp.ToString(), modelCode.ToString()) != 0) && (String.Compare(props[1], modelCode.ToString())) == 0)
                {
                    DMSType type = ModelCodeHelper.GetTypeFromModelCode(modelCode);
                    if (type == 0)
                    {
                        FindChildren(modelCode);
                    }
                    else
                    {
                        tipovi = new List<ModelCode>();
                        tipovi.Add(modelCode);
                    }

                }
            }


            return new List<ModelCode>();
        }


        private void FindChildren(ModelCode modelCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0x");
            List<ModelCode> retCodes = new List<ModelCode>();

            long lmc = (long)modelCode;
            string smc = String.Format("0x{0:x16}", lmc);

            string[] newS = smc.Split('x');
            char[] c = (newS[1]).ToCharArray();


            foreach (char ch in c)
            {
                if (ch != '0')
                {
                    sb.Append(ch);
                }
                else
                {
                    break;
                }

            }

            foreach (ModelCode mc in Enum.GetValues(typeof(ModelCode)))
            {
                DMSType type = ModelCodeHelper.GetTypeFromModelCode(mc);
                short sh = (short)mc;
                if ((modelCode != mc) && (sh == 0) && (type != 0))
                {
                    lmc = (long)mc;
                    smc = String.Format("0x{0:x16}", lmc);
                    if (smc.StartsWith(sb.ToString()))
                    {
                        retCodes.Add(mc);

                    }
                }
            }

            tipovi = retCodes;
        }


        private void button_1_Click(object sender, RoutedEventArgs e)
        {

            if (listBox1.SelectedItems == null || Gid1 == 0)
            {
                MessageBox.Show("You must chose a propertie");
                return;
            }

            List<ModelCode> l = new List<ModelCode>();
            foreach (var v in listBox1.SelectedItems)
            {
                l.Add((ModelCode)v);
            }


            richTextBox_1.Text = new Konekcija().GetValues(Gid1, l);

        }




        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItems == null || ModelKod2 == 0)
            {
                MessageBox.Show("You must chose a propertie");
                return;
            }

            List<ModelCode> l = new List<ModelCode>();
            foreach (var v in listBox2.SelectedItems)
            {
                l.Add((ModelCode)v);
            }
            richTextBox_2.Text = new Konekcija().GetExtentValues(ModelKod2, l);

        }





        private void button_3_Click(object sender, RoutedEventArgs e)
        {
            if (listBox3.SelectedItems == null || AtributID == 0 || Gid3 == 0 || Tip == 0)
            {
                MessageBox.Show("You must chose a propertie");
                return;
            }

            List<ModelCode> l = new List<ModelCode>();
            foreach (var v in listBox3.SelectedItems)
            {
                l.Add((ModelCode)v);
            }

            Association association = new Association();
            association.PropertyId = AtributID;
            association.Type = Tip;

            richTextBox_3.Text = new Konekcija().GetRelatedValues(Gid3, association, l);
        }

        
    }
}
