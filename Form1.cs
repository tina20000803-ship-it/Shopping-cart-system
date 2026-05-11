using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)

        {
            菜單.Items.Add("蜂蜜原味鬆餅$0080");
            菜單.Items.Add("巧克力香蕉鬆餅$0120");
            菜單.Items.Add("草莓奶油鬆餅$0150");
            菜單.Items.Add("宇治金時抹茶鬆餅$0140");
            菜單.Items.Add("鮪魚玉米鹹鬆餅$0110");
            菜單.Items.Add("辣味卡拉雞鬆餅$0100");
        }
        private void button1_Click(object sender, EventArgs e)

        {
            if (菜單.SelectedItem != null)
                購物車.Items.Add(菜單.SelectedItem);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (購物車.SelectedItem != null)
                購物車.Items.Remove(購物車.SelectedItem);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            int totalQty = 購物車.Items.Count;
            int totalMoney = 0;
            richTextBox1.AppendText("🥞 鬆餅專賣店 結帳明細\n");
            richTextBox1.AppendText("----------------------\n");
            Dictionary<string, int> cart = new Dictionary<string, int>();
            foreach (var item in 購物車.Items)
            {
                string str = item.ToString();
                int n = str.IndexOf("$");
                string name = str.Substring(0, n);
                if (cart.ContainsKey(name))
                    cart[name]++;
                else
                    cart[name] = 1;
            }
            foreach (var kv in cart)
            {
                string name = kv.Key;
                int qty = kv.Value;
                int price = 0;
                foreach (var item in 購物車.Items)
                {
                    if (item.ToString().Contains(name))
                    {
                        int n = item.ToString().IndexOf("$");
                        price = int.Parse(item.ToString().Substring(n + 1, 4));
                        break;
                    }
                }
                int subtotal = price * qty;
                totalMoney += subtotal;
                richTextBox1.AppendText($"【{name}】\n   數量:{qty}  單價:{price}  小計:{subtotal}\n");
            }
            richTextBox1.AppendText("----------------------------------\n");
            richTextBox1.SelectionColor = Color.DarkRed;
            richTextBox1.AppendText($"總點餐份數: {totalQty} 份\n");
            richTextBox1.AppendText($"應付總金額: ${totalMoney}\n");
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 取得目前選中的索引值 (從 0 開始)
            int index = 菜單.SelectedIndex;
            // 避免點到空白處導致錯誤
            if (index == -1) return;
            try
            {
                // 根據索引值載入對應圖片 (假設圖片放在執行檔同目錄下)
                // 索引 0 對應 p1.jpg, 索引 1 對應 p2.jpg...
                string fileName = $"{index + 1}.png";
                // 檢查檔案是否存在，避免程式當掉
                if (System.IO.File.Exists(fileName))
                {
                    pictureBox1.Image = Image.FromFile(fileName);
                }
                else
                {
                    // 如果沒圖，可以清空或放一張預設圖
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("圖片載入失敗: " + ex.Message);
            }
            if (菜單.SelectedItem == null) return;

            // 取得選中的字串，例如 "蜂蜜原味鬆餅$0080"
            string selectedLine = 菜單.SelectedItem.ToString();
            int splitPos = selectedLine.IndexOf('$');

            if (splitPos != -1)
            {
                // 拆解字串並填入輸入框
                textBox1.Text = selectedLine.Substring(0, splitPos);
                // 價格部分去掉 $，轉成純數字顯示
                textBox2.Text = selectedLine.Substring(splitPos + 1).TrimStart('0');
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (菜單.SelectedItem == null) return;
            string target = 菜單.SelectedItem.ToString();
            bool found = 購物車.Items.Contains(target);

            if (found)
                MessageBox.Show($"購物車內已有：{target}");
            else
                MessageBox.Show("尚未加入購物車");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (var item in 購物車.Items) list.Add(item.ToString());

            list.Sort(); // 進行排序

            購物車.Items.Clear();
            foreach (var item in list) 購物車.Items.Add(item);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要清空購物車嗎？", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                購物車.Items.Clear();
                richTextBox1.Clear();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (購物車.SelectedItem != null)
            {
                // 取得目前選中的內容 (例如: "草莓奶油鬆餅$0150")
                string selectedItem = 購物車.SelectedItem.ToString();
                int currentIndex = 購物車.SelectedIndex;

                // 這裡示範簡易的「更正」：彈出輸入視窗詢問要改為什麼
                // 實務上通常是彈出一個對話框，這裡用簡單的 InputBox 概念
                // 如果不想寫複雜視窗，也可以設計成：選中後，按「更正」直接改換成「菜單」目前選中的項目

                if (菜單.SelectedItem != null)
                {
                    string newItem = 菜單.SelectedItem.ToString();
                    // 執行更正：移除舊的，在原位置插入新的
                    購物車.Items.RemoveAt(currentIndex);
                    購物車.Items.Insert(currentIndex, newItem);

                    MessageBox.Show($"已將項目更正為：{newItem}");
                }
                else
                {
                    MessageBox.Show("請先在左側菜單選擇想要更換成哪種口味，再按更正。");
                }
            }
            else
            {
                MessageBox.Show("請先從購物車選中一個要修改的項目。");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 1. 檢查輸入是否為空
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("請輸入完整品項名稱與價格");
                return;
            }

            // 2. 處理價格格式 (確保它是數字，並補足四位數如 0160)
            if (int.TryParse(textBox2.Text, out int price))
            {
                string priceStr = price.ToString("D4"); // D4 會自動補零變成 4 位數，例如 160 -> 0160
                string newItem = $"{textBox1.Text}${priceStr}";

                // 3. 加入菜單 ListBox
                菜單.Items.Add(newItem);

                // 4. 清空輸入框方便下一次輸入
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show($"商品【{newItem}】已成功上架！");
            }
            else
            {
                MessageBox.Show("價格請輸入數字");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 1. 檢查是否有選中要更正的項目
            int index = 菜單.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("請先從左側菜單選中一個要更正的商品");
                return;
            }

            // 2. 檢查輸入框是否為空
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("品項名稱與價格不可為空");
                return;
            }

            // 3. 處理價格格式 (補零至四位數)
            if (int.TryParse(textBox2.Text, out int price))
            {
                string priceStr = price.ToString("D4"); // 格式化為 0080 這種樣子
                string updatedItem = $"{textBox1.Text}${priceStr}";

                // 4. 【關鍵】更正該索引位置的內容
                菜單.Items[index] = updatedItem;

                MessageBox.Show("商品資訊已更新！");
            }
            else
            {
                MessageBox.Show("價格請輸入有效數字");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 1. 檢查是否有選中要刪除的品項
            int index = 菜單.SelectedIndex;

            if (index != -1)
            {
                // 取得該品項名稱，用於提示訊息
                string selectedItem = 菜單.SelectedItem.ToString();

                // 2. 彈出確認視窗 (增加操作友善性分數)
                DialogResult result = MessageBox.Show(
                    $"確定要將【{selectedItem}】從菜單移除嗎？",
                    "刪除確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // 3. 執行刪除
                    菜單.Items.RemoveAt(index);

                    // 4. 清空輸入框 (避免殘留已刪除的資料)
                    textBox1.Clear();
                    textBox2.Clear();
                    pictureBox1.Image = null;

                    MessageBox.Show("商品已成功下架。");
                }
            }
            else
            {
                MessageBox.Show("請先從左側菜單選中一個要刪除的商品。");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 1. 檢查輸入框是否為空
            string keyword =textBox1.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("請先輸入要查詢的商品名稱關鍵字");
                return;
            }

            bool found = false;

            // 2. 使用迴圈在「菜單」清單中搜尋
            for (int i = 0; i < 菜單.Items.Count; i++)
            {
                // 取得該項目的文字內容
                string itemText = 菜單.Items[i].ToString();

                // 3. 判斷是否包含關鍵字 (忽略大小寫以增加友善性)
                if (itemText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // 找到後，將該項目設為選取狀態
                    菜單.SelectedIndex = i;
                    found = true;

                    MessageBox.Show($"查詢成功！已為您選中：{itemText}");
                    break; // 找到第一個就停止搜尋，如果要找全部可以移除 break
                }
            }

            // 4. 如果都沒找到
            if (!found)
            {
                MessageBox.Show($"搜尋不到包含「{keyword}」的商品。");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            List<string> items = 菜單.Items.Cast<string>().ToList();
            items.Sort();
            菜單.Items.Clear();
            foreach (var item in items) 菜單.Items.Add(item);
        }
        private void SaveAndOpenFile(string title, string content)
        {
            try
            {
                // 檔名加上時間戳記，避免重複
                string fileName = $"{title}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = System.IO.Path.Combine(Application.StartupPath, fileName);

                // 寫入內容
                System.IO.File.WriteAllText(filePath, content, Encoding.UTF8);

                // 詢問使用者是否開啟
                if (MessageBox.Show($"{title}已存檔至：\n{fileName}\n\n是否立即開啟查看？",
                    "列印完成", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("notepad.exe", filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("列印失敗: " + ex.Message);
            }
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("======= 🥞 鬆餅專賣店 價目表 =======");
            sb.AppendLine($"列印時間：{DateTime.Now:yyyy/MM/dd HH:mm}");
            sb.AppendLine("------------------------------------");

            foreach (var item in 菜單.Items)
            {
                sb.AppendLine(item.ToString());
            }

            sb.AppendLine("------------------------------------");
            sb.AppendLine("        (以上為目前供應品項)        ");

            SaveAndOpenFile("店內菜單", sb.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("======= 🥞 鬆餅專賣店 購物車 =======");
            sb.AppendLine($"列印時間：{DateTime.Now:yyyy/MM/dd HH:mm}");
            sb.AppendLine("------------------------------------");

            foreach (var item in 購物車.Items)
            {
                sb.AppendLine(item.ToString());
            }

            sb.AppendLine("------------------------------------");
            sb.AppendLine("        (以上為目前購物車品項)        ");

            SaveAndOpenFile("購物車", sb.ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("======= 🥞 鬆餅專賣店 購物車 =======");
            sb.AppendLine($"列印時間：{DateTime.Now:yyyy/MM/dd HH:mm}");
            sb.AppendLine("------------------------------------");

            foreach (var item in 購物車.Items)
            {
                sb.AppendLine(item.ToString());
            }

            sb.AppendLine("------------------------------------");
            sb.AppendLine("        (以上為目前購物車品項)        ");

            SaveAndOpenFile("購物車", sb.ToString());
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (菜單.SelectedItem == null) return;
            string target = 菜單.SelectedItem.ToString();
            bool found = 購物車.Items.Contains(target);

            if (found)
                MessageBox.Show($"購物車內已有：{target}");
            else
                MessageBox.Show("尚未加入購物車");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (菜單.SelectedItem != null)
                購物車.Items.Add(菜單.SelectedItem);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (購物車.SelectedItem != null)
                購物車.Items.Remove(購物車.SelectedItem);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (var item in 購物車.Items) list.Add(item.ToString());

            list.Sort(); // 進行排序

            購物車.Items.Clear();
            foreach (var item in list) 購物車.Items.Add(item);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (購物車.SelectedItem != null)
            {
                // 取得目前選中的內容 (例如: "草莓奶油鬆餅$0150")
                string selectedItem = 購物車.SelectedItem.ToString();
                int currentIndex = 購物車.SelectedIndex;

                // 這裡示範簡易的「更正」：彈出輸入視窗詢問要改為什麼
                // 實務上通常是彈出一個對話框，這裡用簡單的 InputBox 概念
                // 如果不想寫複雜視窗，也可以設計成：選中後，按「更正」直接改換成「菜單」目前選中的項目

                if (菜單.SelectedItem != null)
                {
                    string newItem = 菜單.SelectedItem.ToString();
                    // 執行更正：移除舊的，在原位置插入新的
                    購物車.Items.RemoveAt(currentIndex);
                    購物車.Items.Insert(currentIndex, newItem);

                    MessageBox.Show($"已將項目更正為：{newItem}");
                }
                else
                {
                    MessageBox.Show("請先在左側菜單選擇想要更換成哪種口味，再按更正。");
                }
            }
            else
            {
                MessageBox.Show("請先從購物車選中一個要修改的項目。");
            }
        }
    }
}
