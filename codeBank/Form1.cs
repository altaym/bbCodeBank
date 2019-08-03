using codeBank.Data;
using codeBank.Model;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;
using codeBank.Utils;
using System.IO;

namespace codeBank
    {
    public partial class Form1 : Form
        {
        bbSrv srv = new bbSrv();
        bool bListBoxEdit = false, bMove = false;
        int mId = -1; //move id

        Parca p;
        Properties.Settings Ayar = Properties.Settings.Default;

        public Form1()
            {
            InitializeComponent();

            this.AcceptButton = tsAra as IButtonControl;
            tsTxtAra.GotFocus += TsTxtAra_GotFocus;
            tsTxtAra.LostFocus += TsTxtAra_LostFocus;
            tsTxtAra.Text = "Ara...";

            }



        #region arama işlemleri
        private void TsTxtAra_LostFocus(object sender, EventArgs e)
            {
            if (string.IsNullOrWhiteSpace(tsTxtAra.Text))
                tsTxtAra.Text = "Ara...";
            }

        private void TsTxtAra_GotFocus(object sender, EventArgs e)
            {
            if (tsTxtAra.Text == "Ara...")
                {
                tsTxtAra.Text = "";
                }
            }

        private void tsTxtAra_KeyUp(object sender, KeyEventArgs e)
            {
            if (e.KeyCode == Keys.Enter)
                {
                tsAra.PerformClick();
                }
            }

        private void tsAra_Click(object sender, EventArgs e)
            {
            listBox1.DataSource = srv.listParca2(tsTxtAra.Text);
            //listBox1.Items.Clear();
            //foreach (var item in srv.listParca2(tsTxtAra.Text))
            //    {
            //    listBox1.Items.Add(item);
            //    }

            }

        private void tsTxtAra_Click(object sender, EventArgs e)
            {
            tsTxtAra.Text = "";
            }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
            {

            tsl.Text = "";
            this.Text = "bbCodeBank";

            TreeNode root = new TreeNode("Kategoriler");
            root.Tag = 0;
            root.ImageIndex = 1;
            treeViewDoldur(root, 0);
            treeView1.Nodes.Add(root);
            //treeView1.ExpandAll();
            if (Ayar.KategorilerAcik)
                treeView1.ExpandAll();
            else
                treeView1.Nodes[0].Expand();

            EditorAyarlari();
            bbSplitContainer2.Panel1.Padding = new Padding(0, 0, 10, 0);
            bbSplitContainer2.Panel2.Padding = new Padding(0, 0, 10, 0);

            textBox1.Enabled = bListBoxEdit;

            
            }

        private void Kaydet()
            {
            saveToolStripButton.PerformClick();
            }

        #region richTextBox

        void EditorAyarlari()
            {
            panel1.Controls.Add(richTextBox1);
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.TextChanged += RichTextBox1_TextChanged;
            richTextBox1.WrapMode = WrapMode.None;
            richTextBox1.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // NUMBER MARGIN
            InitNumberMargin();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();

            // DRAG DROP
            InitDragDropFile();

            // DEFAULT FILE
            LoadDataFromFile("../../Form1.cs");

            // INIT HOTKEYS
            InitHotkeys();

            //Sözcük Kaydırma Aktif
            sozcukKaydirma.Checked = true;
            richTextBox1.WrapMode = WrapMode.Word;
            richTextBox1.CaretForeColor = Color.White;

            }

        private void InitColors()
            {
            richTextBox1.SetSelectionBackColor(true, IntToColor(0x114D9C));
            }
        private void InitHotkeys()
            {

            HotKeyManager.AddHotKey(this, Kaydet, Keys.S, true, false, false);

            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(this, OpenFindDialog, Keys.F, true, false, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.R, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.H, true);
            HotKeyManager.AddHotKey(this, Uppercase, Keys.U, true);
            HotKeyManager.AddHotKey(this, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(this, ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(this, ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(this, ZoomDefault, Keys.D0, true);
            HotKeyManager.AddHotKey(this, CloseSearch, Keys.Escape);

            // remove conflicting hotkeys from scintilla
            richTextBox1.ClearCmdKey(Keys.Control | Keys.F);
            richTextBox1.ClearCmdKey(Keys.Control | Keys.R);
            richTextBox1.ClearCmdKey(Keys.Control | Keys.H);
            richTextBox1.ClearCmdKey(Keys.Control | Keys.L);
            richTextBox1.ClearCmdKey(Keys.Control | Keys.U);

            }

        private void InitSyntaxColoring()
            {

            // Configure the default style
            richTextBox1.StyleResetDefault();
            richTextBox1.Styles[Style.Default].Font = "Consolas";
            richTextBox1.Styles[Style.Default].Size = 12;
            richTextBox1.Styles[Style.Default].BackColor = IntToColor(0x212121);
            richTextBox1.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            richTextBox1.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            richTextBox1.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            richTextBox1.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            richTextBox1.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            richTextBox1.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            richTextBox1.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            richTextBox1.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            richTextBox1.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            richTextBox1.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            richTextBox1.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            richTextBox1.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            richTextBox1.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            richTextBox1.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            richTextBox1.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            richTextBox1.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            richTextBox1.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            richTextBox1.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            richTextBox1.Lexer = Lexer.Cpp;

            richTextBox1.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            richTextBox1.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

            }


        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin()
            {
            richTextBox1.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            richTextBox1.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            richTextBox1.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            richTextBox1.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = richTextBox1.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            richTextBox1.MarginClick += TextArea_MarginClick;
            }

        private void InitBookmarkMargin()
            {
            richTextBox1.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = richTextBox1.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = richTextBox1.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

            }

        private void InitCodeFolding()
            {

            richTextBox1.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            richTextBox1.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            richTextBox1.SetProperty("fold", "1");
            richTextBox1.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            richTextBox1.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            richTextBox1.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            richTextBox1.Margins[FOLDING_MARGIN].Sensitive = true;
            richTextBox1.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
                {
                richTextBox1.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                richTextBox1.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
                }

            // Configure folding markers with respective symbols
            richTextBox1.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            richTextBox1.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            richTextBox1.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            richTextBox1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            richTextBox1.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            richTextBox1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            richTextBox1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            richTextBox1.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
            {
            if (e.Margin == BOOKMARK_MARGIN)
                {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = richTextBox1.Lines[richTextBox1.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                    {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                    }
                else
                    {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                    }
                }
            }

        #endregion

        #region Drag & Drop File

        public void InitDragDropFile()
            {
            richTextBox1.AllowDrop = true;
            richTextBox1.DragEnter += delegate (object sender, DragEventArgs e)
                {
                    if (e.Data.GetDataPresent(DataFormats.FileDrop))
                        e.Effect = DragDropEffects.Copy;
                    else
                        e.Effect = DragDropEffects.None;
                    };
            richTextBox1.DragDrop += delegate (object sender, DragEventArgs e)
                {
                    // get file drop
                    if (e.Data.GetDataPresent(DataFormats.FileDrop))
                        {
                        Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                        if (a != null)
                            {
                            string path = a.GetValue(0).ToString();
                            LoadDataFromFile(path);
                            }
                        }
                    };
            }

        private void LoadDataFromFile(string path)
            {
            if (File.Exists(path))
                {
                FileName.Text = Path.GetFileName(path);
                richTextBox1.Text = File.ReadAllText(path);
                }
            }

        #endregion

        #region Uppercase / Lowercase

        private void Lowercase()
            {
            int start = richTextBox1.SelectionStart;
            int end = richTextBox1.SelectionEnd;

            richTextBox1.ReplaceSelection(richTextBox1.GetTextRange(start, end - start).ToLower());
            richTextBox1.SetSelection(start, end);
            }

        private void Uppercase()
            {
            int start = richTextBox1.SelectionStart;
            int end = richTextBox1.SelectionEnd;

            richTextBox1.ReplaceSelection(richTextBox1.GetTextRange(start, end - start).ToUpper());
            richTextBox1.SetSelection(start, end);
            }

        #endregion

        #region Indent / Outdent

        private void Indent()
            {
            GenerateKeystrokes("{TAB}");
            }

        private void Outdent()
            {
            GenerateKeystrokes("+{TAB}");
            }

        private void GenerateKeystrokes(string keys)
            {
            HotKeyManager.Enable = false;
            richTextBox1.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
            }

        #endregion

        #region Zoom

        private void ZoomIn()
            {
            richTextBox1.ZoomIn();
            }

        private void ZoomOut()
            {
            richTextBox1.ZoomOut();
            }

        private void ZoomDefault()
            {
            richTextBox1.Zoom = 0;
            }


        #endregion

        #region hizliAramaCubugu

        bool SearchIsOpen = false;

        private void OpenSearch()
            {
            SearchManager.SearchBox = TxtSearch;
            SearchManager.TextArea = richTextBox1;

            if (!SearchIsOpen)
                {
                SearchIsOpen = true;
                InvokeIfNeeded(delegate ()
                    {
                        PanelSearch.Visible = true;
                        TxtSearch.Text = SearchManager.LastSearch;
                        TxtSearch.Focus();
                        TxtSearch.SelectAll();
                        });
                }
            else
                {
                InvokeIfNeeded(delegate ()
                    {
                        TxtSearch.Focus();
                        TxtSearch.SelectAll();
                        });
                }
            }
        private void CloseSearch()
            {
            if (SearchIsOpen)
                {
                SearchIsOpen = false;
                InvokeIfNeeded(delegate ()
                    {
                        PanelSearch.Visible = false;
                        //CurBrowser.GetBrowser().StopFinding(true);
                        });
                }
            }
        private void BtnCloseSearch_Click(object sender, EventArgs e)
            {
            CloseSearch();
            }

        private void BtnNextSearch_Click_1(object sender, EventArgs e)
            {
            SearchManager.Find(true, false);
            }

        private void BtnPrevSearch_Click_1(object sender, EventArgs e)
            {
            SearchManager.Find(false, false);
            }

        private void TxtSearch_TextChanged_1(object sender, EventArgs e)
            {
            SearchManager.Find(true, true);
            }

        private void TxtSearch_KeyDown_1(object sender, KeyEventArgs e)
            {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
                {
                SearchManager.Find(true, false);
                }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
                {
                SearchManager.Find(false, false);
                }
            }

        #endregion

        #region Find & Replace Dialog

        private void OpenFindDialog()
            {

            }
        private void OpenReplaceDialog()
            {


            }

        #endregion

        #region Utils

        public static Color IntToColor(int rgb)
            {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
            }

        public void InvokeIfNeeded(Action action)
            {
            if (this.InvokeRequired)
                {
                this.BeginInvoke(action);
                }
            else
                {
                action.Invoke();
                }
            }

        #endregion


        private void RichTextBox1_TextChanged(object sender, EventArgs e)
            {
            //throw new NotImplementedException();
            }


        #endregion

        #region treview
        void treeViewDoldur(TreeNode root, int ustKategoriId)
            {
            foreach (var k in srv.lKategori(ustKategoriId))
                {
                TreeNode node = new TreeNode(k.Adi);
                node.Tag = k.Id;
                node.ImageIndex = 2;
                treeViewDoldur(node, k.Id);
                root.Nodes.Add(node);
                }
            }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
            {
            TreeNode node = e.Node;
            //if (bListBoxEdit == true){
            //    bListBoxEdit = false;
            //    textBox1.Enabled = false;
            //    listBox1.Enabled = true;
            //    return;
            //    }
            if (node.Tag == null)
                return;
            richTextBox1.Text = ""; // node.Tag + "-" + node.Text + "\n";
            textBox1.Enabled = false;
            textBox1.Text = "";
            listBox1.Items.Clear();
            foreach (var item in srv.listParca2((int)node.Tag))
                {
                listBox1.Items.Add(item);
                }

            //listBox1.DataSource = srv.listParca2((int)node.Tag);

            }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
            {
            treeView1.LabelEdit = false;
            TreeNode tn = treeView1.SelectedNode;
            if (e.Label != null
                && tn.Text != e.Label
                && e.Label.IndexOfAny(new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' }) == -1
                && e.Label.Trim() != ""
                )
                {
                tsl.Text = "Değişiklikler Kaydediliyor...";

                if ((int)tn.Tag == -1)
                    {
                    int r = srv.AddKategori(e.Label, (int)tn.Parent.Tag);
                    if (r != 0)
                        {
                        tn.Tag = r;
                        tn.EndEdit(true);
                        tsl.Text = "Değişiklikler Kaydedildi";
                        

                        }
                    }
                else
                if (srv.RenameKategori((int)tn.Tag, e.Label) != 0)
                    {
                    tn.EndEdit(true);
                    tsl.Text = "Değişiklikler Kaydedildi";
                    }
                }
            else
                {
                tsl.Text = "Kontrol ederek yeniden deneyiniz";
                e.CancelEdit = true;
                return;
                }
            }

        #endregion

        #region listBox
        void getVeri()
            {
            richTextBox1.Text = textBox1.Text = tsl.Text = "";
            if (bListBoxEdit == true || bMove == true || listBox1.SelectedItem ==null) 
                return;
            Parca p = srv.GetParca(((bbItems)listBox1.SelectedItem).Value);
            tsl.Text = textBox1.Text = p.Aciklama;
            tsl.Text += "   (" + p.KayitTarihi + " => " + p.KayitEden + ")";
            richTextBox1.Text = p.Veri.Trim(' ');
            }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
            mId = listBox1.SelectedIndex;
            getVeri();
            }

        private void listBox1_Format(object sender, ListControlConvertEventArgs e)
            {
            if (bListBoxEdit == true)
                return;
            if (e.ListItem is bbItems)
                {
                e.Value = ((bbItems)e.ListItem).ToString();
                }
            else
                {
                e.Value = "Bilinmeyen Kategori";
                }
            }
        #endregion

        #region toolTipMenu
        private void cutToolStripButton_Click(object sender, EventArgs e)
            {
            richTextBox1.Cut();
            }

        private void copyToolStripButton_Click(object sender, EventArgs e)
            {
            richTextBox1.Copy();
            }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
            {
            richTextBox1.Paste();
            }

        private void openToolStripButton_Click(object sender, EventArgs e)
            {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                LoadDataFromFile(openFileDialog1.FileName);
                }
            }

        private void newToolStripButton_Click(object sender, EventArgs e)
            {
            bListBoxEdit = true;
            listBox1.Enabled = !bListBoxEdit;
            textBox1.Enabled = bListBoxEdit;

            textBox1.Focus();
            textBox1.Text = "Başlık Giriniz";
            }

        private void yeniKayıtToolStripMenuItem_Click(object sender, EventArgs e)
            {
            bListBoxEdit = true;
            listBox1.Enabled = !bListBoxEdit;
            textBox1.Enabled = bListBoxEdit;

            textBox1.Focus();
            textBox1.Text = "Başlık Giriniz";

            }

        private void kayıtSilToolStripMenuItem1_Click(object sender, EventArgs e)
            {
            bListBoxEdit = true;
            if (srv.DeleteParca(((bbItems)listBox1.SelectedItem).Value) != 0)
                {
                listBox1.Items.Remove(listBox1.SelectedItem);
                bListBoxEdit = false;
                }
            else
                {
                bListBoxEdit = false;
                return;
                }
            }

        private void yenidenAdlandırToolStripMenuItem_Click(object sender, EventArgs e)
            {
            treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
            }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
            {
            string t = "Muhammet";
            tsl.Text =
            t.IndexOfAny(new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' }).ToString();
            }

        private void saveToolStripButton_Click(object sender, EventArgs e)
            {
            #region veriKontrol
            if (treeView1.SelectedNode.Tag == null)
                {
                tsl.Text = "Önce Bir Kategori Seçmelisiz.";
                return;
                }
            if (string.IsNullOrEmpty(richTextBox1.Text))
                {
                tsl.Text = "Kod Paneli boş olamaz.";
                return;
                }
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || textBox1.Text.Trim() == "Başlık Giriniz")
                {
                tsl.Text = "Kod için bir başlık/açıklama girmelisiz";
                return;
                }
            #endregion

            if (bListBoxEdit == false)
                {
                p = srv.GetParca(((bbItems)listBox1.SelectedItem).Value);
                p.Aciklama = textBox1.Text.Trim();
                p.Veri = richTextBox1.Text;
                if (srv.SaveParca(p) != 0)
                    tsl.Text = "Bilgiler Başarıyla Güncellendi.";
                bListBoxEdit = true;
                }
            else
                {
                p = new Parca();
                p.Aciklama = textBox1.Text.Trim();
                p.Veri = richTextBox1.Text;
                p.Kategori_Id = (int)treeView1.SelectedNode.Tag;

                if (srv.AddParca(p) != 0)
                    {
                    listBox1.Items.Insert(0, p.Aciklama);
                    richTextBox1.Text = textBox1.Text = "";
                    tsl.Text = "Bilgiler Başarıyla Eklendi.";
                    }
                bListBoxEdit = false;
                textBox1.Enabled = bListBoxEdit;
                listBox1.Enabled = !bListBoxEdit;
                }

            }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
            {
            treeView1.ExpandAll();
            }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
            {
            treeView1.CollapseAll();
            }

        /// <summary>
        /// Kategori Ekle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
            {

            TreeNode eklenen = new TreeNode();
            eklenen.Text = "Yeni Kategori";
            eklenen.Tag = -1;

            TreeNode secilen = treeView1.SelectedNode;
            secilen.Nodes.Add(eklenen);
            treeView1.SelectedNode = eklenen;
            treeView1.LabelEdit = true;

            treeView1.SelectedNode.BeginEdit();

            }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
            {
            TreeNode td = treeView1.SelectedNode;
            if (td.Tag == null)
                return;
            if (srv.DeleteKategori((int)td.Tag) == 1)
                treeView1.SelectedNode.Remove();
            }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
            {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                LoadDataFromFile(openFileDialog1.FileName);
                }
            }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.Cut();
            }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.Copy();
            }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.Paste();
            }

        private void satırSeçToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Line line = richTextBox1.Lines[richTextBox1.CurrentLine];
            richTextBox1.SetSelection(line.Position + line.Length, line.Position);
            }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.SelectAll();
            }

        private void seçimiTemizleToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.SetEmptySelection(0);
            }

        private void girintiToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Indent();
            }

        private void girintiyiAzaltToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Outdent();
            }

        private void büyükHarfToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Uppercase();
            }

        private void küçükHarfToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Lowercase();
            }

        private void hızlıAraToolStripMenuItem_Click(object sender, EventArgs e)
            {
            OpenSearch();
            }

        private void bulToolStripMenuItem_Click(object sender, EventArgs e)
            {
            OpenFindDialog();
            }

        private void bulVeDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
            {
            OpenReplaceDialog();
            }

        private void sozcukKaydirmaToolStripMenuItem_Click(object sender, EventArgs e)
            {
            sozcukKaydirma.Checked = !sozcukKaydirma.Checked;
            richTextBox1.WrapMode = sozcukKaydirma.Checked ? WrapMode.Word : WrapMode.None;
            }

        private void klavuzuGösterToolStripMenuItem_Click(object sender, EventArgs e)
            {
            klavuzuGöster.Checked = !klavuzuGöster.Checked;
            richTextBox1.IndentationGuides = klavuzuGöster.Checked ? IndentView.LookBoth : IndentView.None;
            }

        private void boşluklarıGösterToolStripMenuItem_Click(object sender, EventArgs e)
            {
            bosluklarıGöster.Checked = !bosluklarıGöster.Checked;
            richTextBox1.ViewWhitespace = bosluklarıGöster.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
            }

        private void yakınlaştırToolStripMenuItem_Click(object sender, EventArgs e)
            {
            ZoomIn();
            }

        private void uzaklaştırToolStripMenuItem_Click(object sender, EventArgs e)
            {
            ZoomOut();
            }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
            {
            ZoomDefault();
            }

        private void tümünüDaraltToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.FoldAll(FoldAction.Contract);
            }

        private void tümünüGenişletToolStripMenuItem_Click(object sender, EventArgs e)
            {
            richTextBox1.FoldAll(FoldAction.Expand);
            }

        #endregion



        #region drapDrop işlemleri
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
            {
            if (listBox1.Items.Count == 0)
                return;
            getVeri();


            if (MouseButtons == MouseButtons.Left)// && listBox1.SelectedIndex == mId ) 
                {
                bMove = true;
                this.listBox1.DoDragDrop(this.listBox1.SelectedItem, DragDropEffects.Move);
                bMove = false;
                //mId = listBox1.SelectedIndex;
                }

            }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
            {
            TreeNode targetNode = this.treeView1.GetNodeAt(this.treeView1.PointToClient(new Point(e.X, e.Y)));

            if (targetNode == null)
                {
                return;
                }
            //if (targetNode.Level > 0) {
            //    targetNode = targetNode.Parent;
            //}

            bbItems data = (bbItems)e.Data.GetData(typeof(bbItems));
            if (data == null)
                {
                return;
                }
            bMove = true;

            if (srv.moveParca(data.Value, (int)targetNode.Tag) > 0)
                {
                this.listBox1.Items.Remove(data);
                tsl.Text = string.Format("{1} Veri {0} kategorisine başarıyla taşındı.", targetNode.Text, data.Value);

                }
            bMove = false;
            }

        private void listBox1_DragOver(object sender, DragEventArgs e)
            {
            if (e.KeyState == 1)
                {
                e.Effect = DragDropEffects.Move;
                }
            }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
            {
            frmAyar frm = new frmAyar();
            frm.ShowDialog();
            }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
            {
            Application.Exit();
            }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
            {
            e.Effect = DragDropEffects.Move;
            }
        #endregion
        }
    }
