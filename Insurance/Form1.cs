using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;


namespace Insurance
{
    public partial class Form1 : Form
    {
        public Person[] people = new Person[1];
        bool add_flag = false;
        bool red_flag = false;


        public Form1()
        {
            InitializeComponent();
            full_list();
        }

        



        public void but_add_click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                add_flag = true;
                listBox1.Enabled = false;
                but_add.Enabled = false;
                but_delete.Enabled = false;
                but_redact.Enabled = false;
                but_save.Enabled = true;
                but_cancel.Enabled = true;
                text_box_read_only(false);
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }
        }


        public void but_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Пустое поле ФОИ", "Ошибка");
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Пустое поле Телефон", "Ошибка");
                    return;
                }
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Пустое поле Серия и номер паспорта", "Ошибка");
                    return;
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Пустое поле Дата рождения", "Ошибка");
                    return;
                }
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Пустое поле Адрес проживания", "Ошибка");
                    return;
                }
                if (textBox7.Text == "")
                {
                    MessageBox.Show("Пустое поле Вид страхования", "Ошибка");
                    return;
                }
                if (textBox8.Text == "")
                {
                    MessageBox.Show("Пустое поле Номер страхового документа", "Ошибка");
                    return;
                }
                if (textBox9.Text == "")
                {
                    MessageBox.Show("Пустое поле Дата начало страхования", "Ошибка");
                    return;
                }
                if (textBox10.Text == "")
                {
                    MessageBox.Show("Пустое поле Дата конца страхования", "Ошибка");
                    return;
                }

                if (add_flag)
                {
                    bool flag = true;
                    while (flag)
                    {
                        try
                        {
                            Random random = new Random();
                            using (FileStream fs = File.Create("person/" + random.Next(1000, 9999) + ".txt"))
                            {
                                AddText(fs, textBox1.Text);
                                AddText(fs, "\n" + textBox2.Text);
                                AddText(fs, "\n" + textBox3.Text);
                                AddText(fs, "\n" + textBox4.Text);
                                AddText(fs, "\n" + textBox5.Text);
                                AddText(fs, "\n" + textBox6.Text);
                                AddText(fs, "\n" + textBox7.Text);
                                AddText(fs, "\n" + textBox8.Text);
                                AddText(fs, "\n" + textBox9.Text);
                                AddText(fs, "\n" + textBox10.Text);
                            }
                            flag = false;
                        }
                        catch
                        {

                        }
                    }
                    add_flag = false;
                    text_box_read_only(true);
                    textBox10.ReadOnly = true;
                    listBox1.Enabled = true;
                    but_add.Enabled = true;
                    but_delete.Enabled = false;
                    but_redact.Enabled = false;
                    but_save.Enabled = false;
                    but_cancel.Enabled = false;
                    full_list();
                    MessageBox.Show("Добавление прошло успешно!", "Успешно!");
                }
                if (red_flag)
                {
                    int id = listBox1.SelectedIndex;
                    File.Delete(people[id].path);
                    using (FileStream fs = File.Create(people[id].path))
                    {
                        AddText(fs, textBox1.Text);
                        AddText(fs, "\n" + textBox2.Text);
                        AddText(fs, "\n" + textBox3.Text);
                        AddText(fs, "\n" + textBox4.Text);
                        AddText(fs, "\n" + textBox5.Text);
                        AddText(fs, "\n" + textBox6.Text);
                        AddText(fs, "\n" + textBox7.Text);
                        AddText(fs, "\n" + textBox8.Text);
                        AddText(fs, "\n" + textBox9.Text);
                        AddText(fs, "\n" + textBox10.Text);
                    }
                    people[id].name = textBox1.Text;
                    people[id].nember = textBox2.Text;
                    people[id].passport = textBox3.Text;
                    people[id].age = textBox4.Text;
                    people[id].adres = textBox5.Text;
                    people[id].nember_drive = textBox6.Text;
                    people[id].insurance = textBox7.Text;
                    people[id].insurance_nember = textBox8.Text;
                    people[id].date_start = textBox9.Text;
                    people[id].date_end = textBox10.Text;
                    text_box_read_only(true);
                    textBox10.ReadOnly = true;
                    listBox1.Enabled = true;
                    but_add.Enabled = true;
                    but_delete.Enabled = true;
                    but_redact.Enabled = true;
                    but_save.Enabled = false;
                    but_cancel.Enabled = false;
                    red_flag = false;
                    full_list();
                    MessageBox.Show("Редактирование прошло успешно!", "Успешно!");
                }
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }


        }
        public static void AddText(FileStream fs, string value)
        {
            try
            {
                byte[] info = new UTF8Encoding(true).GetBytes(value);
                fs.Write(info, 0, info.Length);
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }
        }

        public void full_list()
        {
            try
            {
                listBox1.Items.Clear();
                string[] file = Directory.GetFiles("person");
                bool flag = true;
                if (file != null)
                {
                    people = new Person[file.Length];
                    int i = 0;
                    foreach (string path in file)
                    {
                        try
                        {
                            using (StreamReader reader = new StreamReader(path))
                            {
                                people[i] = new Person(reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), reader.ReadLine(), path);

                            }
                            i++;
                        }
                        catch 
                        {
                            File.Delete(path);
                            flag = false;
                            full_list();
                            break;
                        }
                    }
                }
                if (flag)
                { 
                    string[] name = new string[people.Length];
                for (int i = 0; i < people.Length; i++) name[i] = people[i].name;
                listBox1.Items.AddRange(name);
                }
                
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }
        }

        public void but_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (add_flag)
                {
                    add_flag = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    text_box_read_only(true);
                    textBox10.ReadOnly = true;
                    listBox1.Enabled = true;
                    but_add.Enabled = true;
                    but_delete.Enabled = false;
                    but_redact.Enabled = false;
                    but_save.Enabled = false;
                    but_cancel.Enabled = false;
                    full_list();
                }
                if (red_flag)
                {
                    int id = listBox1.SelectedIndex;
                    text_box_read_only(true);
                    listBox1.Enabled = true;
                    but_add.Enabled = true;
                    but_delete.Enabled = true;
                    but_redact.Enabled = true;
                    but_save.Enabled = false;
                    but_cancel.Enabled = false;
                    textBox1.Text = people[id].name;
                    textBox2.Text = people[id].nember;
                    textBox3.Text = people[id].passport;
                    textBox4.Text = people[id].age;
                    textBox5.Text = people[id].adres;
                    textBox6.Text = people[id].nember_drive;
                    textBox7.Text = people[id].insurance;
                    textBox8.Text = people[id].insurance_nember;
                    textBox9.Text = people[id].date_start;
                    textBox10.Text = people[id].date_end;
                }
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                int id = listBox1.SelectedIndex;
                but_add.Enabled = true;
                but_delete.Enabled = true;
                but_redact.Enabled = true;
                but_save.Enabled = false;
                but_cancel.Enabled = false;
                textBox1.Text = people[id].name;
                textBox2.Text = people[id].nember;
                textBox3.Text = people[id].passport;
                textBox4.Text = people[id].age;
                textBox5.Text = people[id].adres;
                textBox6.Text = people[id].nember_drive;
                textBox7.Text = people[id].insurance;
                textBox8.Text = people[id].insurance_nember;
                textBox9.Text = people[id].date_start;
                textBox10.Text = people[id].date_end;
            

        }

        public void but_redact_Click(object sender, EventArgs e)
        {
            try
            {
                text_box_read_only(false);
                but_add.Enabled = false;
                but_delete.Enabled = false;
                but_redact.Enabled = false;
                but_save.Enabled = true;
                but_cancel.Enabled = true;
                red_flag = true;
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }
        }
        public void text_box_read_only(bool flag)
        {
            try
            {
                if (flag)
                {
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    textBox8.ReadOnly = true;
                    textBox9.ReadOnly = true;
                    textBox10.ReadOnly = true;
                }
                else
                {
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    textBox4.ReadOnly = false;
                    textBox5.ReadOnly = false;
                    textBox6.ReadOnly = false;
                    textBox7.ReadOnly = false;
                    textBox8.ReadOnly = false;
                    textBox9.ReadOnly = false;
                    textBox10.ReadOnly = false;
                }
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }
        }

        public void but_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = listBox1.SelectedIndex;
                using (StreamReader reader = new StreamReader(people[id].path))
                {
                    
                }
                File.Delete(people[id].path);
                full_list();
                MessageBox.Show("Удаление успешно выполнено", "Успешно!");
            }
            catch
            {
                MessageBox.Show("Error", "Ошибка");
            }

        }
    }
}
