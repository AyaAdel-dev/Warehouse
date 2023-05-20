using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EntityProject
{
    public partial class Form1 : Form
    {
        Model1 ent = new Model1();
        int counter = 1;
        DateTime datetime = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            EntryDate.Text = datetime.ToString("dd/MM/yyyy");
            dateExchange.Text = datetime.ToString("dd/MM/yyyy");
            dateProductByTime.Text = datetime.ToString("dd/MM/yyyy");
            dateExpireDate.Text = datetime.ToString("dd/MM/yyyy");
            dateTransaction.Text = datetime.ToString("dd/MM/yyyy");
            dateTransactionReport.Text = datetime.ToString("dd/MM/yyyy");
            //-----------------------------------------------
            List<int> list = new List<int>();
            foreach (Entry_Product epID in ent.Entry_Product)
            {
                list.Add(epID.Entry_id);
            }
            if (list.Count > 0)
            {
                int[] docId = list.ToArray();
                entryDocumentId.Text = (docId.Max() + 1).ToString();
            }
            else
            {
                entryDocumentId.Text = "1";
            }
            //-----------------------------------------------
            List<int> list1 = new List<int>();
            foreach (Exchange_document edID in ent.Exchange_document)
            {
                list1.Add(edID.exchange_id);
            }
            if (list1.Count > 0)
            {
                int[] docId = list.ToArray();
                exchangeID.Text = (docId.Max() + 1).ToString();
            }
            else
            {
                exchangeID.Text = "1";
            }
            //-----------------------------------------------
            List<int> list2 = new List<int>();
            foreach (transaction_log edID in ent.transaction_log)
            {
                list2.Add(edID.transaction_id);
            }
            if (list2.Count > 0)
            {
                int[] docId = list.ToArray();
                transactionProductTextBox.Text = (docId.Max() + 1).ToString();
            }
            else
            {
                transactionProductTextBox.Text = "1";
            }
            //-----------------------------------------------
            var product = from d in ent.Products
                          select d.product_code;
            foreach (var prd in product)
            {
                if (!comboBox1.Items.Contains(prd) && !comboBox2.Items.Contains(prd))
                {
                    comboBox1.Items.Add(prd);
                    comboBox2.Items.Add(prd);
                }

            }
            foreach (Product prod in ent.Products)
            {
                productNameEntry.Items.Add(prod.name);
                productReportComboBox.Items.Add(prod.name);
                ProductNameByTime.Items.Add(prod.name);
                productNameExpireDate.Items.Add(prod.name);
                //productNameExchange.Items.Add(prod.name);
            }
            var store = from d in ent.Stores
                        select d.name;
            foreach (var str in store)
            {
                comboBox4.Items.Add(str);
                comboBox5.Items.Add(str);
                entryStoreName.Items.Add(str);
                storeReport.Items.Add(str);
                storeNameExchange.Items.Add(str);
                storeProductReport.Items.Add("Select all");
                storeProductReport.Items.Add(str);
                fistStoreTransaction.Items.Add(str);
                secondStoreTransaction.Items.Add(str);
            }
            var suppliers = from d in ent.Suppliers
                            select d.name;
            foreach (var sps in suppliers)
            {
                comboBoxNameUpdate.Items.Add(sps);
                comboBoxNameDelete.Items.Add(sps);
                supplierEntryPermission.Items.Add(sps);
                // suplierNameExchange.Items.Add(sps);
            }
            var customers = from d in ent.Customers select d.name;
            foreach (var crs in customers)
            {
                if (!deleteNameCustomer.Items.Contains(crs) && !updateNameCustomer.Items.Contains(crs))
                {
                    updateNameCustomer.Items.Add(crs);
                    deleteNameCustomer.Items.Add(crs);
                    customerNameExchange.Items.Add(crs);
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.product_code = int.Parse(textBox1.Text);
            product.name = textBox2.Text;
            product.unit = textBox3.Text;
            //Model1 Ent = new Model1();
            var AvailableID = (from d in ent.Products where d.product_code == product.product_code select d).FirstOrDefault();
            if (AvailableID == null)
            {
                ent.Products.Add(product);
                ent.SaveChanges();
                MessageBox.Show("Product Added Successfully");
                textBox1.Text = textBox2.Text = textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var products = from d in ent.Products
                           select d.product_code;
            foreach (var prd in products)
            {
                comboBox1.Items.Add(prd);
                comboBox2.Items.Add(prd);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox1.Text);
            Product product = ent.Products.Find(ID);
            if (product != null)
            {
                textBox5.Text = product.name;
                textBox4.Text = product.unit;

            }
        }

        private void update_Click_1(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox1.Text);
            //Model1 Ent = new Model1();
            Product product = (ent.Products.Where(d => d.product_code == ID).Select(d => d)).FirstOrDefault();
            if (product != null)
            {
                product.name = textBox5.Text;
                product.unit = textBox4.Text;
                ent.SaveChanges();
                MessageBox.Show("Product updated Successfully");
                textBox4.Text = textBox5.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var products = from d in ent.Products
                           select d.product_code;
            foreach (var prd in products)
            {
                if (!comboBox1.Items.Contains(prd) && !comboBox2.Items.Contains(prd))
                {
                    comboBox1.Items.Add(prd);
                    comboBox2.Items.Add(prd);
                }
            }
        }



        private void deleteproductbtn_Click(object sender, EventArgs e)
        {
            int RemovedRow = 0;
            //Model1 Ent = new Model1();
            var product = from d in ent.Products
                          where d.name == textBox8.Text
                          select d;
            foreach (var prd in product)
            {
                ent.Products.Remove(prd);
                RemovedRow++;
            }
            comboBox1.Items.RemoveAt(comboBox2.SelectedIndex);
            comboBox2.Items.RemoveAt(comboBox2.SelectedIndex);
            ent.SaveChanges();
            textBox8.Text = textBox7.Text = "";
            MessageBox.Show(RemovedRow + textBox8.Text + " product Removed");
            var products = from d in ent.Products
                           select d.product_code;
            foreach (var prd in products)
            {
                if (!comboBox1.Items.Contains(prd) && !comboBox2.Items.Contains(prd))
                {
                    comboBox1.Items.Add(prd);
                    comboBox2.Items.Add(prd);
                }
            }
        }



        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int ID = int.Parse(comboBox2.Text);
            Product product = ent.Products.Find(ID);
            if (product != null)
            {

                textBox8.Text = product.name;
                textBox7.Text = product.unit;

            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Store store = new Store();
            store.address = textBox14.Text;
            store.name = textBox9.Text;
            store.manager = textBox6.Text;
            //Model1 Ent = new Model1();
            var AvailableID = (from d in ent.Stores where d.name == store.name select d).FirstOrDefault();
            if (AvailableID == null)
            {
                ent.Stores.Add(store);
                ent.SaveChanges();
                MessageBox.Show("Store Added Successfully");
                textBox14.Text = textBox9.Text = textBox6.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var stores = from d in ent.Stores
                         select d.address;
            foreach (var prd in stores)
            {
                comboBox4.Items.Add(prd);
                comboBox5.Items.Add(prd);
            }
        }



        private void updateBtn_Click(object sender, EventArgs e)
        {
            string name = comboBox4.Text;
            //Model1 Ent = new Model1();
            Store store = (ent.Stores.Where(d => d.name == name).Select(d => d)).FirstOrDefault();
            if (store != null)
            {
                store.address = textBox11.Text;
                store.manager = textBox10.Text;
                ent.SaveChanges();
                MessageBox.Show("Store updated Successfully");
                textBox11.Text = textBox10.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var stores = from d in ent.Stores
                         select d.name;
            foreach (var prd in stores)
            {
                if (!comboBox4.Items.Contains(prd) && !comboBox5.Items.Contains(prd))
                {
                    comboBox4.Items.Add(prd);
                    comboBox5.Items.Add(prd);
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBox4.Text;
            Store store = ent.Stores.Find(name);
            if (store != null)
            {

                textBox11.Text = store.address;
                textBox10.Text = store.manager;

            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int RemovedRow = 0;
            //Model1 Ent = new Model1();
            var store = from d in ent.Stores
                        where d.address == textBox13.Text
                        select d;
            foreach (var str in store)
            {
                ent.Stores.Remove(str);
                RemovedRow++;
            }
            comboBox4.Items.RemoveAt(comboBox5.SelectedIndex);
            comboBox5.Items.RemoveAt(comboBox5.SelectedIndex);
            ent.SaveChanges();
            textBox13.Text = textBox12.Text = "";
            MessageBox.Show(RemovedRow + textBox8.Text + " Removed");
            var stores = from d in ent.Stores
                         select d.name;
            foreach (var str in stores)
            {
                if (!comboBox4.Items.Contains(str) && !comboBox5.Items.Contains(str))
                {
                    comboBox4.Items.Add(str);
                    comboBox5.Items.Add(str);
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBox5.Text;
            Store store = ent.Stores.Find(name);
            if (store != null)
            {

                textBox13.Text = store.address;
                textBox12.Text = store.manager;

            }
        }

        private void addSupplier_Click(object sender, EventArgs e)
        {
            Supplier supplier = new Supplier();
            supplier.name = nameSupplierAdd.Text;
            supplier.telephone = telephoneSupplierAdd.Text;
            supplier.fax = faxSupplierAdd.Text;
            supplier.phone = phoneSupplierAdd.Text;
            supplier.mail = mailSupplierAdd.Text;
            supplier.website = websiteSupplierAdd.Text;
            //Model1 Ent = new Model1();
            var AvailableID = (from d in ent.Suppliers where d.name == supplier.name select d).FirstOrDefault();
            if (AvailableID == null)
            {
                ent.Suppliers.Add(supplier);
                ent.SaveChanges();
                MessageBox.Show("Supplier Added Successfully");
                nameSupplierAdd.Text = telephoneSupplierAdd.Text = faxSupplierAdd.Text = phoneSupplierAdd.Text = mailSupplierAdd.Text = websiteSupplierAdd.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var suppliers = from d in ent.Suppliers
                            select d.name;
            foreach (var sps in suppliers)
            {
                if (!comboBoxNameDelete.Items.Contains(sps) && !comboBoxNameUpdate.Items.Contains(sps))
                {
                    comboBoxNameDelete.Items.Add(sps);
                    comboBoxNameUpdate.Items.Add(sps);
                }
            }
        }

        private void comboBoxNameUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBoxNameUpdate.Text;
            Supplier supplier = ent.Suppliers.Find(name);
            if (supplier != null)
            {

                telephoneSupplierUpdate.Text = supplier.telephone;
                faxSupplierUpdate.Text = supplier.fax;
                phoneSupplierUpdate.Text = supplier.phone;
                mailSupplierUpdate.Text = supplier.mail;
                websiteSupplierUpdate.Text = supplier.website;

            }
        }

        private void UpdateSupplier_Click(object sender, EventArgs e)
        {
            string name = comboBoxNameUpdate.Text;
            //Model1 Ent = new Model1();
            Supplier supplier = (ent.Suppliers.Where(d => d.name == name).Select(d => d)).FirstOrDefault();
            if (supplier != null)
            {

                supplier.telephone = telephoneSupplierUpdate.Text;
                supplier.fax = faxSupplierUpdate.Text;
                supplier.phone = phoneSupplierUpdate.Text;
                supplier.mail = mailSupplierUpdate.Text;
                supplier.website = websiteSupplierUpdate.Text;
                ent.SaveChanges();
                MessageBox.Show("Store updated Successfully");
                nameSupplierAdd.Text = telephoneSupplierAdd.Text = faxSupplierAdd.Text = phoneSupplierAdd.Text = mailSupplierAdd.Text = websiteSupplierAdd.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var suppliers = from d in ent.Suppliers
                            select d.name;
            foreach (var sps in suppliers)
            {
                if (!comboBoxNameDelete.Items.Contains(sps) && !comboBoxNameUpdate.Items.Contains(sps))
                {
                    comboBoxNameDelete.Items.Add(sps);
                    comboBoxNameUpdate.Items.Add(sps);
                }
            }
        }

        private void DeleteBtnSupplier_Click(object sender, EventArgs e)
        {
            int RemovedRow = 0;
            //Model1 Ent = new Model1();
            var supplier = from d in ent.Suppliers
                           where d.telephone == telephoneDeleteSupplier.Text
                           select d;
            foreach (var str in supplier)
            {
                ent.Suppliers.Remove(str);
                RemovedRow++;
            }
            comboBoxNameUpdate.Items.RemoveAt(comboBoxNameDelete.SelectedIndex);
            comboBoxNameDelete.Items.RemoveAt(comboBoxNameDelete.SelectedIndex);
            ent.SaveChanges();
            telephoneDeleteSupplier.Text = faxDeleteSupplier.Text = phoneDeleteSupplier.Text = mailDeleteSupplier.Text = websiteDeleteSupplier.Text = "";
            MessageBox.Show(RemovedRow + " " + comboBoxNameDelete.SelectedItem.ToString() + " Removed");
            var suppliers = from d in ent.Suppliers
                            select d.name;
            foreach (var sps in suppliers)
            {
                if (!comboBoxNameDelete.Items.Contains(sps) && !comboBoxNameUpdate.Items.Contains(sps))
                {
                    comboBoxNameDelete.Items.Add(sps);
                    comboBoxNameUpdate.Items.Add(sps);
                }
            }
        }

        private void comboBoxNameDelete_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBoxNameDelete.Text;
            Supplier supplier = ent.Suppliers.Find(name);
            if (supplier != null)
            {

                telephoneDeleteSupplier.Text = supplier.telephone;
                faxDeleteSupplier.Text = supplier.fax;
                phoneDeleteSupplier.Text = supplier.phone;
                mailDeleteSupplier.Text = supplier.mail;
                websiteDeleteSupplier.Text = supplier.website;

            }

        }

        private void addBtnCustomers_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.name = addNameCustomer.Text;
            customer.telephone = addTelephoneCustomer.Text;
            customer.fax = addFaxCustomer.Text;
            customer.phone = addPhoneCustomer.Text;
            customer.mail = addMailCustomer.Text;
            customer.website = addWebsiteCustomer.Text;
            //Model1 Ent = new Model1();
            var AvailableID = (from d in ent.Suppliers where d.name == customer.name select d).FirstOrDefault();
            if (AvailableID == null)
            {
                ent.Customers.Add(customer);
                ent.SaveChanges();
                updateNameCustomer.Items.Add(addNameCustomer.Text);
                deleteNameCustomer.Items.Add(addNameCustomer.Text);
                MessageBox.Show("Customer Added Successfully");
                addNameCustomer.Text = addTelephoneCustomer.Text = addFaxCustomer.Text = addPhoneCustomer.Text = addMailCustomer.Text = addWebsiteCustomer.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }

        private void updateNameCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = updateNameCustomer.Text;
            Customer customer = ent.Customers.Find(name);
            if (customer != null)
            {

                updateTelephoneCustomer.Text = customer.telephone;
                updateFaxCustomer.Text = customer.fax;
                updatePhoneCustomer.Text = customer.phone;
                updateMailCustomer.Text = customer.mail;
                updateWebsiteCustomer.Text = customer.website;

            }
        }

        private void deleteNameCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = deleteNameCustomer.Text;
            Customer customer = ent.Customers.Find(name);
            if (customer != null)
            {

                deleteTelephoneCustomer.Text = customer.telephone;
                deleteFaxCustomer.Text = customer.fax;
                deletePhoneCustomer.Text = customer.phone;
                deleteMailCustomer.Text = customer.mail;
                deleteWebsiteCustomer.Text = customer.website;

            }
        }

        private void updateBtnCustomer_Click(object sender, EventArgs e)
        {
            string name = updateNameCustomer.Text;
            //Model1 Ent = new Model1();
            Customer customer = (ent.Customers.Where(d => d.name == name).Select(d => d)).FirstOrDefault();
            if (customer != null)
            {

                customer.telephone = updateTelephoneCustomer.Text;
                customer.fax = updateFaxCustomer.Text;
                customer.phone = updatePhoneCustomer.Text;
                customer.mail = updateMailCustomer.Text;
                customer.website = updateWebsiteCustomer.Text;
                ent.SaveChanges();
                MessageBox.Show("Customer updated Successfully");
                updateTelephoneCustomer.Text = updateFaxCustomer.Text = updatePhoneCustomer.Text = updateMailCustomer.Text = updateWebsiteCustomer.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
            var customers = from d in ent.Customers
                            select d.name;
            foreach (var crs in customers)
            {
                if (!deleteNameCustomer.Items.Contains(crs) && !updateNameCustomer.Items.Contains(crs))
                {
                    updateNameCustomer.Items.Add(crs);
                    deleteNameCustomer.Items.Add(crs);
                }

            }
        }

        private void deleteBtnCustomer_Click(object sender, EventArgs e)
        {
            //MessageBox.Show();
            int RemovedRow = 0;
            //Model1 Ent = new Model1();
            var customer = from d in ent.Customers
                           where d.name == deleteNameCustomer.Text
                           select d;
            foreach (var crs in customer)
            {
                ent.Customers.Remove(crs);

                RemovedRow++;
            }
            updateNameCustomer.Items.RemoveAt(deleteNameCustomer.SelectedIndex);
            deleteNameCustomer.Items.RemoveAt(deleteNameCustomer.SelectedIndex);

            ent.SaveChanges();
            deleteTelephoneCustomer.Text = deleteFaxCustomer.Text = deletePhoneCustomer.Text = deleteMailCustomer.Text = deleteWebsiteCustomer.Text = "";
            MessageBox.Show(RemovedRow + " Removed");
            var customers = from d in ent.Customers
                            select d.name;
            foreach (var crs in customers)
            {
                if (!deleteNameCustomer.Items.Contains(crs) && !updateNameCustomer.Items.Contains(crs))
                {
                    updateNameCustomer.Items.Add(crs);
                    deleteNameCustomer.Items.Add(crs);
                }

            }
        }

        private void addPermissionEntry_Click(object sender, EventArgs e)
        {

            if (dateTimePicker1.Text.Length > 0 && dateTimePicker2.Text.Length > 0 && countEntry.Text != "")
            {

                listBox1.Items.Add($"{counter},{countEntry.Text},{productNameEntry.Text},{dateTimePicker1.Value.ToString("yyyy-MM-dd")},{dateTimePicker2.Value.ToString("yyyy-MM-dd")}");
                counter++;
            }
            else
            {
                MessageBox.Show("Please Insert data ");
            }

        }

        private void SaveEntry_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (countEntry.Text != "" && productNameEntry.Items.Count != 0)
            {
                Entry_document entryDocument = new Entry_document();
                entryDocument.Entry_id = int.Parse(entryDocumentId.Text);
                entryDocument.date = DateTime.Parse(datetime.ToString("yyyy-MM-dd"));
                entryDocument.store_name = (string)entryStoreName.SelectedItem;
                entryDocument.supplier_name = (string)supplierEntryPermission.SelectedItem;
                //Model1 Ent = new Model1();
                var AvailableID = (from d in ent.Entry_document where d.Entry_id == entryDocument.Entry_id select d).FirstOrDefault();
                if (AvailableID == null)
                {
                    ent.Entry_document.Add(entryDocument);
                    ent.SaveChanges();
                    // MessageBox.Show("Customer Added Successfully");

                }
                foreach (var item in listBox1.Items)
                {
                    string test = item.ToString();
                    string[] words = test.Split(',');

                    Entry_Product prod = new Entry_Product();
                    prod.Entry_id = int.Parse(entryDocumentId.Text);
                    prod.count_product = int.Parse(words[1]);
                    prod.prod_date = DateTime.Parse(words[3]);
                    prod.expire_date = DateTime.Parse(words[4]);

                    string testing = words[2];
                    //var prod1 = from b in datamodel.Products
                    //            where b.name == words[1]
                    //            select b;
                    var prod2 = ent.Products.FirstOrDefault(user => user.name == testing);
                    prod.Product_id = prod2.product_code;
                    //var counter = prod1.Count();
                    //foreach (var koko in prod1)
                    //{
                    //    prod.Product_id = koko.product_code;
                    //}

                    ent.Entry_Product.Add(prod);
                    ent.SaveChanges();

                }


                MessageBox.Show("Record added");
                entryDocumentId.Text = $"{int.Parse(entryDocumentId.Text) + 1}";
                // textBox26.Text = comboBox8.Text = comboBox9.Text = "";
                listBox1.Items.Clear();

            }
            else
            {
                MessageBox.Show("please complete fields");
            }

        }

        private void storeReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = storeReport.Text;
            Store store = ent.Stores.Find(name);
            if (store != null)
            {
                storeReportLable.Text = "Store: " + store.name;

            }
        }

        private void displayStoreReport_Click(object sender, EventArgs e)
        {
            if (storeReport.SelectedIndex >= 0)
            {

                // MessageBox.Show(storeReport.Text);
                var stores = (ent.Stores.Where(d => d.name == storeReport.Text).Select(d => d)).FirstOrDefault();
                var entryDoc = (ent.Entry_document.Where(d => d.store_name == storeReport.Text).Select(d => d));
                StoreReportListBox.Items.Add("Store Name: " + stores.name);
                StoreReportListBox.Items.Add("Store Address: " + stores.address);
                StoreReportListBox.Items.Add("Store Manager: " + stores.manager);
                foreach (var items in entryDoc)
                {
                    var entryprod = (ent.Entry_Product.Where(d => d.Entry_id == items.Entry_id).Select(d => d));

                    foreach (var item in entryprod)
                    {
                        var product = (ent.Products.Where(d => d.product_code == item.Product_id).Select(d => d));

                        StoreReportListBox.Items.Add("Product count: " + item.count_product);
                        StoreReportListBox.Items.Add("Production Date: " + item.prod_date);
                        StoreReportListBox.Items.Add("Expire Date: " + item.expire_date);

                        foreach (var proditem in product)
                        {
                            StoreReportListBox.Items.Add("Product Name: " + proditem.name);
                            StoreReportListBox.Items.Add("Unit: " + proditem.unit);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(" Please Select Store ");
            }
        }

        private void addExchangeBtn_Click(object sender, EventArgs e)
        {
            if (dateTimePicker4.Text.Length > 0 && dateTimePicker3.Text.Length > 0 && suplierNameExchange.Text != "" && productNameExchange.Text != "")
            {
                var entryDoc = (ent.Entry_document.Where(d => d.store_name == storeNameExchange.Text).Select(d => d));
                foreach (var items in entryDoc)
                {
                    var entryprod = (ent.Entry_Product.Where(d => d.Entry_id == items.Entry_id).Select(d => d));
                    var product = (ent.Products.Where(d => d.name == productNameExchange.Text).Select(d => d)).FirstOrDefault();
                    foreach (var item in entryprod)
                    {
                        if (item.Product_id == product.product_code)
                        {
                            if (int.Parse(countExchange.Text) > item.count_product)
                            {
                                MessageBox.Show("Invalid Product Count");
                            }
                            else
                            {
                                listBox2.Items.Add($"{counter},{countExchange.Text},{productNameExchange.Text},{dateTimePicker4.Value.ToString("yyyy-MM-dd")},{dateTimePicker3.Value.ToString("yyyy-MM-dd")}");
                                counter++;
                            }

                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Please Check data ");
            }
        }

        private void saveExchangeBtn_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (countExchange.Text != "" && productNameExchange.Items.Count != 0)
            {
                Exchange_document exchangeDoc = new Exchange_document();
                exchangeDoc.exchange_id = int.Parse(exchangeID.Text);
                exchangeDoc.exchange_date = DateTime.Parse(datetime.ToString("yyyy-MM-dd"));
                exchangeDoc.store_name = (string)storeNameExchange.SelectedItem;
                exchangeDoc.customer_name = (string)customerNameExchange.SelectedItem;
                var AvailableID = (from d in ent.Exchange_document where d.exchange_id == exchangeDoc.exchange_id select d).FirstOrDefault();
                if (AvailableID == null)
                {
                    ent.Exchange_document.Add(exchangeDoc);
                    ent.SaveChanges();

                }
                if (listBox2.Items.Count > 0)
                {
                    foreach (var item in listBox2.Items)
                    {
                        string test = item.ToString();
                        string[] words = test.Split(',');

                        Exchange_product exchangeProd = new Exchange_product();
                        exchangeProd.Exchange_id = int.Parse(exchangeID.Text);
                        exchangeProd.product_count = int.Parse(words[1]);
                        exchangeProd.supplier_name = suplierNameExchange.Text;
                        //exchangeProd.expire_date = DateTime.Parse(words[4]);

                        string testing = words[2];
                        var prod2 = ent.Products.FirstOrDefault(user => user.name == testing);
                        exchangeProd.Product_id = prod2.product_code;
                        ent.Exchange_product.Add(exchangeProd);
                        ent.SaveChanges();

                    }


                    MessageBox.Show("Record added");
                    exchangeID.Text = $"{int.Parse(exchangeID.Text) + 1}";
                    // textBox26.Text = comboBox8.Text = comboBox9.Text = "";
                    listBox1.Items.Clear();

                }
                else
                {
                    MessageBox.Show("Add Items");
                }
            }
            else
            {
                MessageBox.Show("please complete fields");
            }
        }

        private void storeNameExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (storeNameExchange.SelectedIndex >= 0)
            {
                var entryDoc = (ent.Entry_document.Where(d => d.store_name == storeNameExchange.Text).Select(d => d));
                foreach (var items in entryDoc)
                {
                    var entryprod = (ent.Entry_Product.Where(d => d.Entry_id == items.Entry_id).Select(d => d));
                    if (!suplierNameExchange.Items.Contains(items.supplier_name))
                    {
                        suplierNameExchange.Enabled = true;
                        suplierNameExchange.Items.Add(items.supplier_name);

                    }
                    foreach (var item in entryprod)
                    {
                        var product = (ent.Products.Where(d => d.product_code == item.Product_id).Select(d => d));

                        foreach (var proditem in product)
                        {
                            productNameExchange.Enabled = true;
                            if (!productNameExchange.Items.Contains(proditem.name))
                            {
                                productNameExchange.Items.Add(proditem.name);
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(" Please Select Store ");
            }
        }



        private void productNameExchange_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (productNameExchange.SelectedIndex >= 0)
            {
                var entryDoc = (ent.Entry_document.Where(d => d.store_name == storeNameExchange.Text).Select(d => d));
                foreach (var items in entryDoc)
                {
                    var entryprod = (ent.Entry_Product.Where(d => d.Entry_id == items.Entry_id).Select(d => d));
                    var product = (ent.Products.Where(d => d.name == productNameExchange.Text).Select(d => d)).FirstOrDefault();
                    foreach (var item in entryprod)
                    {
                        if (item.Product_id == product.product_code)
                        {
                            countExchange.Enabled = true;
                            countExchange.Text = $"{item.count_product}";

                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(" Please Select Product ");
            }
        }


        private void displayProductReport_Click(object sender, EventArgs e)
        {

            productReportListBox.Items.Clear();
            if (storeProductReport.Text == "" || storeProductReport.Text == "Select all")
            {
                if (productReportComboBox.SelectedIndex >= 0)
                {
                    var prod = (ent.Products.Where(d => d.name == productReportComboBox.Text).Select(d => d)).FirstOrDefault();

                    var entryProd = (ent.Entry_Product.Where(d => d.Product_id == prod.product_code).Select(d => d));
                    foreach (var item in entryProd)
                    {
                        var entry_doc = (ent.Entry_document.Where(d => d.Entry_id == item.Entry_id).Select(d => d));
                        foreach (var item1 in entry_doc)
                        {
                            //if (item1.store_name != comboBox16.Text) { break; }
                            productReportListBox.Items.Add("store name = " + item1.store_name);
                            productReportListBox.Items.Add("entry date = " + item1.date);
                            productReportListBox.Items.Add("supplier name = " + item1.supplier_name);

                        }
                        productReportListBox.Items.Add("Product count = " + item.count_product);
                        productReportListBox.Items.Add("Product Id = " + prod.product_code);
                        productReportListBox.Items.Add("Productiond date = " + item.prod_date);
                        productReportListBox.Items.Add("expire date = " + item.expire_date);
                        productReportListBox.Items.Add("**********************************");
                    }

                }
                else
                {
                    MessageBox.Show(" Please Select a Product ");
                }
            }
            else
            {
                var prod = (ent.Products.Where(d => d.name == productReportComboBox.Text).Select(d => d)).FirstOrDefault();

                var entryProd = (ent.Entry_Product.Where(d => d.Product_id == prod.product_code).Select(d => d));
                foreach (var item in entryProd)
                {

                    var entry_doc = (ent.Entry_document.Where(d => d.Entry_id == item.Entry_id).Select(d => d));
                    foreach (var item1 in entry_doc)
                    {
                        if (item1.store_name != storeProductReport.Text) { break; }
                        else
                        {
                            productReportListBox.Items.Add("store name = " + item1.store_name);
                            productReportListBox.Items.Add("entry date = " + item1.date);
                            productReportListBox.Items.Add("supplier name = " + item1.supplier_name);
                            productReportListBox.Items.Add("Product count = " + item.count_product);
                            productReportListBox.Items.Add("Product Id = " + prod.product_code);
                            productReportListBox.Items.Add("Productiond date = " + item.prod_date);
                            productReportListBox.Items.Add("expire date = " + item.expire_date);
                            productReportListBox.Items.Add("**********************************");
                        }

                    }

                }
            }
        }

        private void displayBtnProductByTime_Click(object sender, EventArgs e)
        {

            int days = 0;
            if (MoreThanProductByTime.Text == "1 month") days = 30;
            else if (MoreThanProductByTime.Text == "2 months") days = 60;
            else if (MoreThanProductByTime.Text == "3 months") days = 90;
            else if (MoreThanProductByTime.Text == "6 months") days = 180;
            else if (MoreThanProductByTime.Text == "9 months") days = 270;
            else if (MoreThanProductByTime.Text == "1 year") days = 365;
            else if (MoreThanProductByTime.Text == "2 years") days = 730;
            else if (MoreThanProductByTime.Text == "3 years") days = 1095;

            listBox3.Items.Clear();
            if (ProductNameByTime.Text != "" && ProductNameByTime.Text != "")
            {
                if (ProductNameByTime.SelectedIndex >= 0)
                {
                    var prod = (ent.Products.Where(d => d.name == ProductNameByTime.Text).Select(d => d)).FirstOrDefault();

                    var entryProd = (ent.Entry_Product.Where(d => d.Product_id == prod.product_code).Select(d => d));
                    foreach (var item in entryProd)
                    {
                        var itemdate = item.Entry_document.date;

                        var today = DateTime.Now;
                        var diffDATE = today - (itemdate);
                        if (diffDATE.Days > days)
                        {
                            listBox3.Items.Add("store name = " + item.Entry_document.store_name);
                            listBox3.Items.Add("entry date = " + item.Entry_document.date);
                            listBox3.Items.Add("supplier name = " + item.Entry_document.supplier_name);

                            listBox3.Items.Add("Product count = " + item.count_product);
                            listBox3.Items.Add("Product Id = " + prod.product_code);
                            listBox3.Items.Add("Production date = " + item.prod_date);
                            listBox3.Items.Add("expire date = " + item.expire_date);
                            listBox3.Items.Add("**********************************");
                        }
                    }

                }
                else
                {
                    MessageBox.Show(" Please Select a Product ");
                }
            }
        }

        private void displayExpireDate_Click(object sender, EventArgs e)
        {
            int days = 0;
            if (lessThanExpireDate.Text == "1 month") days = 30;
            else if (lessThanExpireDate.Text == "2 months") days = 60;
            else if (lessThanExpireDate.Text == "3 months") days = 90;
            else if (lessThanExpireDate.Text == "6 months") days = 180;
            else if (lessThanExpireDate.Text == "9 months") days = 270;
            else if (lessThanExpireDate.Text == "1 year") days = 365;
            else if (lessThanExpireDate.Text == "2 years") days = 730;
            else if (lessThanExpireDate.Text == "3 years") days = 1095;

            listBox4.Items.Clear();
            if (lessThanExpireDate.Text != "" && productNameExpireDate.Text != "")
            {
                if (productNameExpireDate.SelectedIndex >= 0)
                {
                    var prod = (ent.Products.Where(d => d.name == productNameExpireDate.Text).Select(d => d)).FirstOrDefault();

                    var entryProd = (ent.Entry_Product.Where(d => d.Product_id == prod.product_code).Select(d => d));
                    foreach (var item in entryProd)
                    {
                        var itemdate = item.expire_date;

                        var today = DateTime.Now;
                        var diffDATE = today - (itemdate);
                        if (diffDATE.Days < days)
                        {
                            listBox4.Items.Add("store name = " + item.Entry_document.store_name);
                            listBox4.Items.Add("entry date = " + itemdate);
                            listBox4.Items.Add("supplier name = " + item.Entry_document.supplier_name);

                            listBox4.Items.Add("Product count = " + item.count_product);
                            listBox4.Items.Add("Product Id = " + prod.product_code);
                            listBox4.Items.Add("Production date = " + item.prod_date);
                            listBox4.Items.Add("expire date = " + item.expire_date);
                            listBox4.Items.Add("**********************************");
                        }

                    }

                }
                else
                {
                    MessageBox.Show(" Please Select a Product ");
                }
            }
        }

        private void fistStoreTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (fistStoreTransaction.SelectedIndex >= 0)
            {
                var entryDoc = (ent.Entry_document.Where(d => d.store_name == fistStoreTransaction.Text).Select(d => d));
                foreach (var items in entryDoc)
                {
                    var entryprod = (ent.Entry_Product.Where(d => d.Entry_id == items.Entry_id).Select(d => d));
                    if (!supplierTransaction.Items.Contains(items.supplier_name))
                    {
                        supplierTransaction.Enabled = true;
                        supplierTransaction.Items.Add(items.supplier_name);

                    }
                    foreach (var item in entryprod)
                    {
                        var product = (ent.Products.Where(d => d.product_code == item.Product_id).Select(d => d));

                        foreach (var proditem in product)
                        {
                            productTransaction.Enabled = true;
                            if (!productTransaction.Items.Contains(proditem.name))
                            {
                                productTransaction.Items.Add(proditem.name);
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(" Please Select Store ");
            }
        }

        private void productTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fistStoreTransaction.SelectedIndex >= 0)
            {
                var entryDoc = (ent.Entry_document.Where(d => d.store_name == fistStoreTransaction.Text).Select(d => d));
                foreach (var items in entryDoc)
                {
                    var entryprod = (ent.Entry_Product.Where(d => d.Entry_id == items.Entry_id).Select(d => d));

                    var product = (ent.Products.Where(d => d.name == productTransaction.Text).Select(d => d)).FirstOrDefault();
                    foreach (var item in entryprod)
                    {
                        if (item.Product_id == product.product_code)
                        {
                            countTransaction.Enabled = true;
                            countTransaction.Text = $"{item.count_product}";
                            dateTimePicker6.Value = (DateTime)item.prod_date;
                            dateTimePicker5.Value = (DateTime)item.expire_date;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show(" Please Select Product ");
            }
        }

        private void saveTransaction_Click(object sender, EventArgs e)
        {

            if (countTransaction.Text != "" && fistStoreTransaction.Items.Count != 0)
            {

                transaction_log trans = new transaction_log();
                trans.transaction_label = "Exchange";
                trans.transaction_date = DateTime.Parse((DateTime.Now).ToString("yyyy-MM-dd")); ;
                var prod2 = ent.Products.FirstOrDefault(user => user.name == productTransaction.Text);
                var entry = ent.Entry_Product.FirstOrDefault(id => id.Product_id == prod2.product_code);
                trans.product_id = prod2.product_code;
                trans.entry_id = entry.Entry_id;
                trans.product_count = int.Parse(countTransaction.Text);
                trans.store_name = fistStoreTransaction.Text;
                trans.new_store_name = secondStoreTransaction.Text;
                trans.supplier_name = supplierTransaction.Text;
                trans.production_date = entry.prod_date;
                trans.expiry_date = entry.expire_date;

                ent.transaction_log.Add(trans);
                ent.SaveChanges();

                MessageBox.Show("Record added");
                transactionProductTextBox.Text = $"{int.Parse(transactionProductTextBox.Text) + 1}";

            }
            else
            {
                MessageBox.Show("please complete fields");
            }
        }

        private void displayTransaction_Click(object sender, EventArgs e)
        {
            var translog = ent.transaction_log.Select(d => d);
            foreach (var item in translog)
            {
                if (item.transaction_date > dateTimePicker8.Value && item.transaction_date < dateTimePicker7.Value)
                {
                    listBox5.Items.Clear();
                    var prod2 = ent.Products.FirstOrDefault(user => user.product_code == item.product_id);
                    var proddate = (DateTime)(item.production_date);
                    var expirdate = (DateTime)(item.expiry_date);
                    listBox5.Items.Add("transaction id = " + item.transaction_id);
                    listBox5.Items.Add("transaction date " + item.transaction_date.ToString("dd/MM/yyyy"));
                    listBox5.Items.Add("Product name :" + prod2.name);
                    listBox5.Items.Add("Product id :" + item.product_id);
                    listBox5.Items.Add("Entry permission id :" + item.entry_id);
                    listBox5.Items.Add("Products count" + item.product_count);
                    listBox5.Items.Add("production date" + proddate.ToString("dd/MM/yyyy"));
                    listBox5.Items.Add("expiry date : " + expirdate.ToString("dd/MM/yyyy"));
                    listBox5.Items.Add("Transfered from store :" + item.store_name);
                    listBox5.Items.Add("to store :" + item.new_store_name);
                    listBox5.Items.Add("supplier name : " + item.supplier_name);
                    listBox5.Items.Add("*********************************");
                }
            }

        }
    }
}


