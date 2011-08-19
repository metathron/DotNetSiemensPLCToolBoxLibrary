﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using DotNetSimaticDatabaseProtokollerLibrary;
using DotNetSimaticDatabaseProtokollerLibrary.SettingsClasses.Connections;
using DotNetSimaticDatabaseProtokollerLibrary.SettingsClasses.Datasets;

namespace DotNetSimaticDatabaseProtokollerConfigurationTool.Windows
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class EditProtocolDatasets : UserControl
    {
        public EditProtocolDatasets()
        {
            InitializeComponent();
        }

        //Helper property to get the DatGrid as Property (for use within the DataTemplate...)
        public DataGrid GetGrdDatsetsObject { get { return grdDatasets; } }

        private void cmdAddDataset_Click(object sender, RoutedEventArgs e)
        {
            string val = "Table_" + (ProtokollerConfiguration.ActualConfigInstance.Datasets.Count + 1);
            if (DotNetSiemensPLCToolBoxLibrary.General.InputBox.Show("Tablename", "Name of the Dataset (used as Tablename)", ref val) == System.Windows.Forms.DialogResult.OK)
            {
                DatasetConfig myConfig = new DatasetConfig();
                myConfig.Name = val;
                ProtokollerConfiguration.ActualConfigInstance.Datasets.Add(myConfig);
            }
        }

        private void cmdRemoveDataset_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasets.SelectedItem != null)
                ProtokollerConfiguration.ActualConfigInstance.Datasets.Remove((DatasetConfig) grdDatasets.SelectedItem);
        }

        private void cmdSelectReadBitFromProject_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasets.SelectedItem != null)
            {
                DatasetConfig conf = grdDatasets.SelectedItem as DatasetConfig;
                var tmp = DotNetSiemensPLCToolBoxLibrary.Projectfiles.SelectProjectPart.SelectTAG("");
                if (tmp != null)
                {
                    tmp.LibNoDaveDataType = DotNetSiemensPLCToolBoxLibrary.DataTypes.TagDataType.Bool;
                    conf.TriggerReadBit = tmp;
                }
            }
        }

        private void cmdEditReadBit_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasets.SelectedItem != null)
            {
                DatasetConfig conf = grdDatasets.SelectedItem as DatasetConfig;
                var tmp = DotNetSiemensPLCToolBoxLibrary.Communication.PLCTagEditor.ShowPLCTagEditor(conf.TriggerReadBit);
                if (tmp != null)
                {
                    tmp.LibNoDaveDataType = DotNetSiemensPLCToolBoxLibrary.DataTypes.TagDataType.Bool;
                    conf.TriggerReadBit = tmp;
                }
            }
        }

        private void cmdAddDatasetRow_Click(object sender, RoutedEventArgs e)
        {            
            DatasetConfig conf = grdDatasets.SelectedItem as DatasetConfig;

            ConnectionConfig akConn = null;
            string FieldType = "";
            if (conf.DatasetConfigRows.Count > 0)
            {
                akConn = conf.DatasetConfigRows[conf.DatasetConfigRows.Count - 1].Connection;
                FieldType = conf.DatasetConfigRows[conf.DatasetConfigRows.Count - 1].DatabaseFieldType;
            }

            conf.DatasetConfigRows.Add(new DatasetConfigRow() {DatabaseField = "Row_" + (conf.DatasetConfigRows.Count + 1).ToString(), Connection = akConn, DatabaseFieldType = FieldType});
        }

        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell != null && !cell.IsEditing && !cell.IsReadOnly)
            {
                if (!cell.IsFocused)
                {
                    cell.Focus();
                }
                DataGrid dataGrid = FindVisualParent<DataGrid>(cell);
                if (dataGrid != null)
                {
                    if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                    {
                        if (!cell.IsSelected)
                            cell.IsSelected = true;
                    }
                    else
                    {
                        DataGridRow row = FindVisualParent<DataGridRow>(cell);
                        if (row != null && !row.IsSelected)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }


        private void cmdEditBitInDataRowsList_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasetFields.SelectedItem != null)
            {
                DatasetConfigRow conf = grdDatasetFields.SelectedItem as DatasetConfigRow;
                var tmp = DotNetSiemensPLCToolBoxLibrary.Communication.PLCTagEditor.ShowPLCTagEditor(conf.PLCTag);
                if (tmp != null)
                {
                    conf.PLCTag = tmp;
                }
            }
        }

        private void cmdSelectBitFromProjectInDataRowsList_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasetFields.SelectedItem != null)
            {
                DatasetConfigRow conf = grdDatasetFields.SelectedItem as DatasetConfigRow;
                var tmp = DotNetSiemensPLCToolBoxLibrary.Projectfiles.SelectProjectPart.SelectTAG("");
                if (tmp != null)
                {
                    conf.PLCTag = tmp;
                }
            }
        }

        private void cmdEditQuittBit_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasets.SelectedItem != null)
            {
                DatasetConfig conf = grdDatasets.SelectedItem as DatasetConfig;
                var tmp = DotNetSiemensPLCToolBoxLibrary.Communication.PLCTagEditor.ShowPLCTagEditor(conf.TriggerQuittBit);
                if (tmp != null)
                {
                    tmp.LibNoDaveDataType = DotNetSiemensPLCToolBoxLibrary.DataTypes.TagDataType.Bool;
                    conf.TriggerQuittBit = tmp;
                }
            }
        }

        private void cmdSelectQuitBitFromProject_Click(object sender, RoutedEventArgs e)
        {
            if (grdDatasets.SelectedItem != null)
            {
                DatasetConfig conf = grdDatasets.SelectedItem as DatasetConfig;
                var tmp = DotNetSiemensPLCToolBoxLibrary.Projectfiles.SelectProjectPart.SelectTAG("");
                if (tmp != null)
                {
                    tmp.LibNoDaveDataType = DotNetSiemensPLCToolBoxLibrary.DataTypes.TagDataType.Bool;
                    conf.TriggerQuittBit = tmp;
                }
            }
        }

        private void cmdRemoveDatasetRow_Click(object sender, RoutedEventArgs e)
        {
            DatasetConfig conf = grdDatasets.SelectedItem as DatasetConfig;
            if (grdDatasetFields.SelectedItem != null)
                conf.DatasetConfigRows.Remove((DatasetConfigRow)grdDatasetFields.SelectedItem);
        
        }

        private void txt_Enter_ValueUpdater(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BindingExpression be = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
                ((TextBox)sender).SelectAll();
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

                          
    }
}