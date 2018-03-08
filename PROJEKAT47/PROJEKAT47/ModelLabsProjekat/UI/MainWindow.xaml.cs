
using System;
using System.Collections.Generic;
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
using FTN.Common;
using FTN.ServiceContracts;
using System.Xml;
using System.Collections;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModelResourcesDesc mrd = new ModelResourcesDesc();
        private List<long> lista = null;
        ModelCode mkod = 0;
        TestGda gda = new TestGda();
        List<long> gids = new List<long>();
        List<string> list = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            GetValue();
            GetExtendValue();


        }

     



        private void GetExtendValue()
        {
            List<ModelCode> mc = new List<ModelCode>();


            mc.Add(ModelCode.ACLINESEGMENT);
            mc.Add(ModelCode.BAY);
            mc.Add(ModelCode.CNODE);
            mc.Add(ModelCode.SERIESCOMPENSATOR);
            mc.Add(ModelCode.TERMINAL);
            mc.Add(ModelCode.DCLINESEGMENT);

            comboBox2.ItemsSource = mc;


            gids = gda.TestGetExtentValuesAllTypes();


            comboBox3.ItemsSource = gids;

            List<ModelCode> pomocna = new List<ModelCode>();
            List<ModelCode> properties = new List<ModelCode>();
   
            for (int i = 0; i < mc.Count(); i++)
            {
                pomocna = mrd.GetAllPropertyIds(mc[i]);
                for (int x = 0; x < pomocna.Count(); x++)
                {
                    properties.Add(pomocna[x]);
                }

            }
            comboBox5.ItemsSource = properties;


            List<ModelCode> mc1 = new List<ModelCode>();
            mc1.Add(ModelCode.BAY);
            mc1.Add(ModelCode.SERIESCOMPENSATOR);
            mc1.Add(ModelCode.ACLINESEGMENT);
            mc1.Add(ModelCode.DCLINESEGMENT);
            mc1.Add(ModelCode.TERMINAL);
            mc1.Add(ModelCode.CNODE);
            mc1.Add(0);
            comboBox4.ItemsSource = mc1;

        }
        private void GetValue()
        {
            StringBuilder s = new StringBuilder();
            List<long> gids = new List<long>();
            List<long> gids2 = new List<long>();
          
           gids = gda.TestGetExtentValuesAllTypes();
           comboBox1.ItemsSource = gids;

          
        }

        private void button1_Click(object sender, RoutedEventArgs e) //za getValues
        {
            StringBuilder s = new StringBuilder();
           
          
        

            long gid = 0;
            gid = (long)comboBox1.SelectedItem;
            ResourceDescription rd = new ResourceDescription();
            
            rd = gda.GetValues(gid);
           
            int j = 0;
            int i;
            
            short tip = ModelCodeHelper.ExtractTypeFromGlobalId(gid);
            List<ModelCode> prop = mrd.GetAllPropertyIds((DMSType)tip);
       
            textBox1.Text = "Tip resursa:" + ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(gid)).ToString() + "\n";

            textBox1.Text += "ID: " + s.Append(String.Format("0x{0:x16}", gid)).ToString() + "\n";

            for (i = 0; i < rd.Properties.Count(); i++)
            {
                if (rd.Properties[i].Type != PropertyType.ReferenceVector)
                {

                    textBox1.Text += "Property id: " + prop[i].ToString();
                        textBox1.Text+=", value: " + rd.Properties[i].ToString() + "\n";
                      
                 
                }

                else
                {
                        textBox1.Text += "Property id: " + prop[i].ToString();
                        for (int x = 0; x < rd.Properties[i].PropertyValue.LongValues.Count(); x++)
                        {

                            textBox1.Text += ", value: " + rd.Properties[i].PropertyValue.LongValues[x].ToString() + "\n";

                        }
             
                }
             

            }
        }


        private void button2_Click(object sender, RoutedEventArgs e)//za getExtendValues
        {
            StringBuilder s = new StringBuilder();
            ResourceDescription rd = new ResourceDescription();

            mkod = (ModelCode)comboBox2.SelectedItem;
            lista = gda.GetExtentValues(mkod);
            long globalId = 0;
            globalId = (long)mkod;

            string str = s.Append(String.Format("0x{0:x16}", globalId)).ToString();

            
            for (int i = 0; i < lista.Count(); i++)
            {
                rd = gda.GetValues(lista[i]);
               
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(lista[i]);
                List<ModelCode> properties = mrd.GetAllPropertyIds((DMSType)type);

                textBox2.Text += "TYPE: " + ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(lista[i])).ToString() + "\n" + "ID:" + str;
           
               
                for (int x = 0; x < rd.Properties.Count(); x++)
                {
                    if (rd.Properties[x].Type == PropertyType.ReferenceVector)
                    {


                        textBox2.Text += "\n";
                        textBox2.Text += properties[x].ToString();
                        textBox2.Text += ": ";

                        for (int j = 0; j < rd.Properties[x].PropertyValue.LongValues.Count(); j++)
                        {
                            textBox2.Text += rd.Properties[x].PropertyValue.LongValues[j].ToString();

                            textBox2.Text += " ";
                        }

                    }
                    else
                    {

                        textBox2.Text += "\n";
                        textBox2.Text += properties[x].ToString();
                        textBox2.Text += ": ";
                        textBox2.Text += rd.Properties[x].ToString();
                    }
                }
                textBox2.Text += "\n\n";


            }
 
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder s = new StringBuilder();
            ResourceDescription rd = null;

            ModelCode mc = new ModelCode();
            List<long> ids = new List<long>();
            long gid = 0;

            gid = (long)comboBox3.SelectedItem;
            mc = (ModelCode)comboBox5.SelectedItem;
            Association asoc = new Association();
            if (comboBox4.SelectedItem == null)
            {
                asoc.Type = 0;

            }
            else
            {
                asoc.Type = (ModelCode)comboBox4.SelectedItem;

            }

            asoc.PropertyId = mc;

           
                ids = gda.GetRelatedValues(gid, asoc);

                textBox3.Text = "";
                for (int i = 0; i < ids.Count; i++)
                {

                    rd = gda.GetValues(ids[i]);
                    short type = ModelCodeHelper.ExtractTypeFromGlobalId(ids[i]);
                    List<ModelCode> properties = mrd.GetAllPropertyIds((DMSType)type);

                    textBox3.Text += "ID: ";
                    textBox3.Text += s.Append(String.Format("0x{0:x16}", gid)).ToString() + "\n";
                    textBox3.Text += "TYPE: ";
                    textBox3.Text += ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(ids[i])).ToString();

                    for (int x = 0; x < rd.Properties.Count(); x++)
                    {
                        if (rd.Properties[x].Type == PropertyType.ReferenceVector)
                        {


                            textBox3.Text += "\n";
                            textBox3.Text += properties[x].ToString();
                            textBox3.Text += ": ";

                            for (int j = 0; j < rd.Properties[x].PropertyValue.LongValues.Count(); j++)
                            {
                                textBox3.Text += rd.Properties[x].PropertyValue.LongValues[j].ToString();

                                textBox3.Text += " ";
                            }

                        }
                        else
                        {
                            textBox3.Text += "\n";
                            textBox3.Text += properties[x].ToString();
                            textBox3.Text += ": ";
                            textBox3.Text += rd.Properties[x].ToString();
                        }

                    }
                    textBox3.Text += "\n\n";


                }


            }



        }
    }
