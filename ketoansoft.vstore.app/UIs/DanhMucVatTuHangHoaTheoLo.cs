﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ketoansoft.app.Class.Data;
using DevExpress.XtraEditors;
using ketoansoft.app.Components.clsVproUtility;
using System.Collections;
using ketoansoft.app.Class.Global;
using ketoansoft.app.Diaglog;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ketoansoft.app
{
    public partial class DanhMucVatTuHangHoaTheoLo : DevComponents.DotNetBar.Metro.MetroForm
    {
        dbVstoreAppDataContext _db = new dbVstoreAppDataContext();
        private KTDMHHLoRepo _KTDMHHLoRepo = new KTDMHHLoRepo();
        public Unit _unit = new Unit();
        private List<int> _listUpdate = new List<int>();

        public DanhMucVatTuHangHoaTheoLo()
        {
            InitializeComponent();
        }

        #region Data
        private void Load_Data()
        {
            try
            {
                _KTDMHHLoRepo = new KTDMHHLoRepo();
                gridData.DataSource = _KTDMHHLoRepo.GetAll();
                //_db = new dbVstoreAppDataContext();
                //gridData.DataSource = null;
                //gridData.DataSource = _db.KT_DMHH_Los;
                //gridData.RefreshDataSource();
                //gridView1.RefreshData();
            }
            catch (Exception) { }
        }
        private void Save_Data(bool msg)
        {
            try
            {
                _KTDMHHLoRepo = new KTDMHHLoRepo();
                int i = 0;
                foreach (int pos in _listUpdate)
                {
                    int id = Utils.CIntDef(gridView1.GetRowCellValue(pos, "ID"), 0);
                    KT_DMHH_Lo obj = _KTDMHHLoRepo.GetById(id);
                    if (obj != null)
                    {
                        obj.MA_DM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_DM"), "");
                        obj.TEN_DM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TEN_DM"), "");
                        obj.DANH_DAU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DANH_DAU"), "");
                        obj.MA_TK = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_TK"), "");
                        obj.MA_DTPN = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_DTPN"), "");
                        obj.MA_LO = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_LO"), "");
                        DateTime? temp = null;
                        if (Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NGAY_LO"), DateTime.MinValue) != DateTime.MinValue)
                            temp = Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NGAY_LO"), DateTime.MinValue);
                        obj.NGAY_LO = temp;
                        temp = null;
                        if (Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "HSD"), DateTime.MinValue) != DateTime.MinValue)
                            temp = Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "HSD"), DateTime.MinValue);
                        obj.HSD = temp;
                        obj.TEN_DTPN = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TEN_DTPN"), "");
                        obj.KY_HIEU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "KY_HIEU"), "");
                        obj.DON_VI = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DON_VI"), "");
                        obj.DG_XUAT = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DG_XUAT"), 0);
                        obj.TON_DK_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_DK_SL"), 0);
                        obj.TON_DK_GT_VND = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_DK_GT_VND"), 0);
                        obj.NHAP_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NHAP_SL"), 0);
                        obj.NHAP_GT_VND = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NHAP_GT_VND"), 0);
                        obj.XUAT_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "XUAT_SL"), 0);
                        obj.XUAT_GT_USD = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "XUAT_GT_USD"), 0);
                        obj.TON_CK_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_CK_SL"), 0);
                        obj.TON_CK_GT_VND = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_CK_GT_VND"), 0);

                        _KTDMHHLoRepo.Update(obj);
                        i++;
                    }
                }
                _listUpdate = new List<int>();
                //if (i > 0 && msg)
                //{
                //    MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Save_Tick()
        {
            try
            {
                int id = Utils.CIntDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID"), 0);
                KT_DMHH_Lo obj = _KTDMHHLoRepo.GetById(id);
                if (obj != null)
                {
                    obj.DANH_DAU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DANH_DAU"), "").Trim() == "T" ? "" : "T";
                    _KTDMHHLoRepo.Update(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Save_Duplicate()
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn copy dòng này thành dòng mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _KTDMHHLoRepo = new KTDMHHLoRepo();
                    KT_DMHH_Lo obj = new KT_DMHH_Lo();
                    obj.MA_DM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_DM"), "");
                    obj.TEN_DM = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TEN_DM"), "");
                    obj.DANH_DAU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DANH_DAU"), "");
                    obj.MA_TK = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_TK"), "");
                    obj.MA_DTPN = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_DTPN"), "");
                    obj.MA_LO = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA_LO"), "");
                    DateTime? temp = null;
                    if (Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NGAY_LO"), DateTime.MinValue) != DateTime.MinValue)
                        temp = Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NGAY_LO"), DateTime.MinValue);
                    obj.NGAY_LO = temp;
                    temp = null;
                    if (Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "HSD"), DateTime.MinValue) != DateTime.MinValue)
                        temp = Utils.CDateDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "HSD"), DateTime.MinValue);
                    obj.HSD = temp;
                    obj.TEN_DTPN = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TEN_DTPN"), "");
                    obj.KY_HIEU = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "KY_HIEU"), "");
                    obj.DON_VI = Utils.CStrDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DON_VI"), "");
                    obj.DG_XUAT = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DG_XUAT"), 0);
                    obj.TON_DK_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_DK_SL"), 0);
                    obj.TON_DK_GT_VND = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_DK_GT_VND"), 0);
                    obj.NHAP_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NHAP_SL"), 0);
                    obj.NHAP_GT_VND = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NHAP_GT_VND"), 0);
                    obj.XUAT_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "XUAT_SL"), 0);
                    obj.XUAT_GT_USD = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "XUAT_GT_USD"), 0);
                    obj.TON_CK_SL = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_CK_SL"), 0);
                    obj.TON_CK_GT_VND = Utils.CDblDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TON_CK_GT_VND"), 0);

                    _KTDMHHLoRepo.Create(obj);
                    MessageBox.Show("Đã copy dòng này vào cuối bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Remove_Data()
        {
            try
            {
                //if (_listUpdate.Count > 0)
                //{
                //    MessageBox.Show("Hãy thực hiện lưu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                Save_Data(false);

                _KTDMHHLoRepo = new KTDMHHLoRepo();
                int id = Utils.CIntDef(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID"), 0);

                _KTDMHHLoRepo.Remove(id);

                //MessageBox.Show("Xóa dòng ID:" + Id + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Grid Event
        private void gridData_Load(object sender, EventArgs e)
        {
            Unit.UnitGridview(gridView1);
            Load_Data();

            Load_InfoHeader();
        }
        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            // Sự kiện này để người ta không chuyển qua dòng khác được khi có lỗi xảy ra nè
            // Nó nhận giá trị e.Valid của gridView1_ValidateRow để ứng xử
            // neu e,Valid =True thì nó cho chuyển qua dòng khác hoặc làm tác vụ khác
            // và ngược lại
             e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                bool b = _listUpdate.Exists(a => a == gridView1.FocusedRowHandle);
                if (!b)
                {
                    _listUpdate.Add(gridView1.FocusedRowHandle);
                }
            }));
        }
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            //_unit.Get_ColorTick(gridView1, sender, e);
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Load_InfoRows();
        }
        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //validation
                GridColumn ma_dmCol = view.Columns["MA_DM"];
                GridColumn ma_loCol = view.Columns["MA_LO"];
                GridColumn ma_tkCol = view.Columns["MA_TK"];
                GridColumn ma_dtpnCol = view.Columns["MA_DTPN"];
                string ma_dm = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, ma_dmCol), "");
                string ma_lo = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, ma_loCol), "");
                string ma_tk = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, ma_tkCol), "");
                string ma_dtpn = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, ma_dtpnCol), "");
                if (ma_dm.Trim().Length == 0 || ma_lo.Trim().Length == 0 || ma_tk.Trim().Length == 0 || ma_dtpn.Trim().Length == 0)
                {
                    e.Valid = false;
                    if (ma_dm.Trim().Length == 0)
                        view.SetColumnError(ma_dmCol, "Mã danh mục không được rổng");
                    if (ma_lo.Trim().Length == 0)
                        view.SetColumnError(ma_loCol, "Mã lô không được rổng");
                    if (ma_tk.Trim().Length == 0)
                        view.SetColumnError(ma_tkCol, "Mã tài khoản không được rổng");
                    if (ma_dtpn.Trim().Length == 0)
                        view.SetColumnError(ma_dtpnCol, "Mã đối tượng pháp nhân không được rổng");

                    return;
                }
                _KTDMHHLoRepo = new KTDMHHLoRepo();

                //Kiểm tra đây là dòng dữ liệu mới hay cũ, nếu là mới thì mình insert
                if (view.IsNewItemRow(e.RowHandle))
                {
                    //e.RowHandle trả về giá trị int là thứ tự của dòng hiện tại
                    KT_DMHH_Lo obj = new KT_DMHH_Lo();

                    obj.MA_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_DM"), "");
                    obj.TEN_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_DM"), "");
                    obj.DANH_DAU = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DANH_DAU"), "");
                    obj.MA_TK = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_TK"), "");
                    obj.MA_DTPN = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_DTPN"), "");
                    obj.MA_LO = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_LO"), "");
                    DateTime? temp = null;
                    if (Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "NGAY_LO"), DateTime.MinValue) != DateTime.MinValue)
                        temp = Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "NGAY_LO"), DateTime.MinValue);
                    obj.NGAY_LO = temp;
                    temp = null;
                    if (Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "HSD"), DateTime.MinValue) != DateTime.MinValue)
                        temp = Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "HSD"), DateTime.MinValue);
                    obj.HSD = temp;
                    obj.TEN_DTPN = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_DTPN"), "");
                    obj.KY_HIEU = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "KY_HIEU"), "");
                    obj.DON_VI = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DON_VI"), "");
                    obj.DG_XUAT = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DG_XUAT"), 0);
                    obj.TON_DK_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_DK_SL"), 0);
                    obj.TON_DK_GT_VND = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_DK_GT_VND"), 0);
                    obj.NHAP_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "NHAP_SL"), 0);
                    obj.NHAP_GT_VND = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "NHAP_GT_VND"), 0);
                    obj.XUAT_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "XUAT_SL"), 0);
                    obj.XUAT_GT_USD = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "XUAT_GT_USD"), 0);
                    obj.TON_CK_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_CK_SL"), 0);
                    obj.TON_CK_GT_VND = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_CK_GT_VND"), 0);

                    _KTDMHHLoRepo.Create(obj);

                }
                //Cũ thì update
                else
                {
                    int id = Utils.CIntDef(view.GetRowCellValue(gridView1.FocusedRowHandle, "ID"), 0);
                    KT_DMHH_Lo obj = _KTDMHHLoRepo.GetById(id);
                    if (obj != null)
                    {
                        obj.MA_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_DM"), "");
                        obj.TEN_DM = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_DM"), "");
                        obj.DANH_DAU = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DANH_DAU"), "");
                        obj.MA_TK = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_TK"), "");
                        obj.MA_DTPN = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_DTPN"), "");
                        obj.MA_LO = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "MA_LO"), "");
                        DateTime? temp = null;
                        if (Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "NGAY_LO"), DateTime.MinValue) != DateTime.MinValue)
                            temp = Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "NGAY_LO"), DateTime.MinValue);
                        obj.NGAY_LO = temp;
                        temp = null;
                        if (Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "HSD"), DateTime.MinValue) != DateTime.MinValue)
                            temp = Utils.CDateDef(view.GetRowCellValue(e.RowHandle, "HSD"), DateTime.MinValue);
                        obj.HSD = temp;
                        obj.TEN_DTPN = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "TEN_DTPN"), "");
                        obj.KY_HIEU = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "KY_HIEU"), "");
                        obj.DON_VI = Utils.CStrDef(view.GetRowCellValue(e.RowHandle, "DON_VI"), "");
                        obj.DG_XUAT = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "DG_XUAT"), 0);
                        obj.TON_DK_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_DK_SL"), 0);
                        obj.TON_DK_GT_VND = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_DK_GT_VND"), 0);
                        obj.NHAP_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "NHAP_SL"), 0);
                        obj.NHAP_GT_VND = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "NHAP_GT_VND"), 0);
                        obj.XUAT_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "XUAT_SL"), 0);
                        obj.XUAT_GT_USD = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "XUAT_GT_USD"), 0);
                        obj.TON_CK_SL = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_CK_SL"), 0);
                        obj.TON_CK_GT_VND = Utils.CDblDef(view.GetRowCellValue(e.RowHandle, "TON_CK_GT_VND"), 0);

                        _KTDMHHLoRepo.Update(obj);
                    }

                }
                Load_Data();
            }
            catch (Exception ex)
            {
                e.Valid = false;
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save_Data(true);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn thoát không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    Save_Data(false);
            //    this.Close();
            //}
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            gridView1.ShowFindPanel();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Remove_Data();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            _unit.Get_AllowEdit(gridView1);
        }
        private void btnToCol_Click(object sender, EventArgs e)
        {
            FocusCol();
        }
        private void btnTick_Click(object sender, EventArgs e)
        {
            Save_Tick();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            Save_Duplicate();
            Load_Data();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

        #region Funtion
        private void FocusCol()
        {
            dlogToCol dialog = new dlogToCol(gridView1);
            dialog.ShowDialog();
            string _value = dialog.Result;
            gridView1.FocusedColumn = gridView1.Columns[_value];
        }
        private void Load_InfoHeader()
        {
            txtMonth.Text = Utils.CStrDef(fTerm._month,"");
            txtYear.Text = Utils.CStrDef(fTerm._year, "");
            txtDatabase.Text = Unit.Get_DatabaseName();
            txtCompanyName.Text = Unit.Get_CompanyName();
            txtNumRows.Text = Utils.CStrDef(gridView1.RowCount, "0") + " of " + Utils.CStrDef(gridView1.RowCount, "0");
        }
        private void Load_InfoRows()
        {
            //string[] _arr = {"SO_KU","SO_HD","MA_TK", "MA_DTPN"};
            txtInfoRows.Text = Unit.Get_InfoRows(gridView1, null);
        }
        #endregion

        #region shortcutKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            KeyEventArgs e = new KeyEventArgs(keyData);
            if (e.KeyCode == Keys.F1)
            {
                btnSearch_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                Save_Tick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                Save_Duplicate();
                Load_Data();
            }
            else if (e.KeyCode == Keys.F5)
            {
                Load_Data();
            }
            else if (e.KeyCode == Keys.F6)
            {
                FocusCol();
            }
            else if (e.KeyCode == Keys.S && e.Control)
            {
                Save_Data(true); return true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(null, null);
            }
            else if (e.KeyCode == Keys.Q && e.Control)
            {
                btnExit_Click(null, null);
            }
            else if (e.KeyCode == Keys.F10)
            {
                _unit.Get_AllowEdit(gridView1);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion        

    }
}