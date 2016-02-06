﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Gma.System.MouseKeyHook;

namespace Anathema
{
    public partial class GUIInputCorrelator : DockContent, IInputCorrelatorView
    {
        private InputCorrelatorPresenter InputCorrelatorPresenter;

        private readonly IKeyboardMouseEvents InputHook;    // Input capturing class
        private Boolean UpdatingInputTextBox;

        private Stack<Int32> SelectionIndecies;

        public GUIInputCorrelator()
        {
            InitializeComponent();

            InputCorrelatorPresenter = new InputCorrelatorPresenter(this, new InputCorrelator());

            InputHook = Hook.GlobalEvents();

            UpdatingInputTextBox = false;

            SetVariableSize();
            EnableGUI();
        }

        public void DisplayScanCount(Int32 ScanCount)
        {
            ControlThreadingHelper.InvokeControlAction(ScanToolStrip, () =>
            {
                ScanCountLabel.Text = "Scan Count: " + ScanCount.ToString();
            });
        }

        private void SetVariableSize()
        {
            Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
            VariableSizeValueLabel.Text = Conversions.BytesToMetric((UInt64)VariableSize).ToString();

            InputCorrelatorPresenter.SetVariableSize(VariableSize);
        }

        private void EnableGUI()
        {
            StartScanButton.Enabled = true;
            StopScanButton.Enabled = false;
            VariableSizeTrackBar.Enabled = true;
        }

        private void DisableGUI()
        {
            StartScanButton.Enabled = false;
            StopScanButton.Enabled = true;
            VariableSizeTrackBar.Enabled = false;
        }

        /// <summary>
        /// Clears the display and saves the indicies stack needed to navigate to the current selection
        /// </summary>
        public void ClearDisplay()
        {
            ControlThreadingHelper.InvokeControlAction(InputTreeView, () =>
            {
                SelectionIndecies = GetSelectionIndicies();
                this.InputTreeView.Nodes.Clear();
            });
        }

        public void UpdateDisplay(TreeNode Root)
        {
            ControlThreadingHelper.InvokeControlAction(InputTreeView, () =>
            {
                this.InputTreeView.Nodes.Clear();

                if (Root == null)
                    return;

                this.InputTreeView.Nodes.Add(Root);

                AddContextMenuToNodes(InputTreeView.Nodes);

                // Attempt to restore selection if possible
                InputNode TargetNode = InputTreeView.Nodes.Count == 0 ? null : (InputNode)InputTreeView.Nodes[0];

                // Always throw away the first one, we only allow for a single root
                if (SelectionIndecies.Count != 0)
                    SelectionIndecies.Pop();

                // Try to recover the original selection if it exists
                while (TargetNode != null && SelectionIndecies.Count > 0)
                    TargetNode = TargetNode.GetChildAtIndex(SelectionIndecies.Pop());

                // Update selection
                if (ContainsNode(InputTreeView.Nodes, TargetNode))
                    InputTreeView.SetSelection(TargetNode);

                InputTreeView.ExpandAll();
            });
        }

        private void AddContextMenuToNodes(TreeNodeCollection Nodes)
        {
            if (Nodes == null)
                return;

            foreach (TreeNode Node in Nodes)
            {
                Node.ContextMenuStrip = InputContextMenuStrip;
                AddContextMenuToNodes(Node.Nodes);
            }
        }

        private Boolean ContainsNode(TreeNodeCollection Nodes, TreeNode TargetNode)
        {
            if (Nodes == null)
                return false;

            foreach (TreeNode Node in Nodes)
            {
                if (Node == TargetNode)
                    return true;

                if (ContainsNode(Node.Nodes, TargetNode))
                    return true;
            }
            return false;
        }

        private Stack<Int32> GetSelectionIndicies()
        {
            Stack<Int32> Indicies = new Stack<Int32>();
            TreeNode CurrentNode = InputTreeView.SelectedNode;
            while (CurrentNode != null)
            {
                Indicies.Push(CurrentNode.Index);
                CurrentNode = CurrentNode.Parent;
            }

            return Indicies;
        }

        private void GlobalHookKeyUp(Object Sender, KeyEventArgs E)
        {
            InputCorrelatorPresenter.SetCurrentKey(E.KeyCode);
            UpdatingInputTextBox = true;
            InputTextBox.Text = E.KeyCode.ToString();
        }

        #region Events

        private void StartScanButton_Click(Object Sender, EventArgs E)
        {
            DisableGUI();
            InputCorrelatorPresenter.BeginScan();
        }

        private void StopScanButton_Click(Object Sender, EventArgs E)
        {
            EnableGUI();
            InputCorrelatorPresenter.EndScan();
        }

        private void HandleResize()
        {
            //VariableSizeTrackBar.Width = this.Width / 2 - VariableSizeTrackBar.Location.X;
        }

        private void VariableSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetVariableSize();
        }

        private void InputTextBox_Enter(Object Sender, EventArgs E)
        {
            InputHook.KeyUp += GlobalHookKeyUp;
        }

        private void InputTextBox_Leave(object sender, EventArgs e)
        {
            InputHook.KeyUp -= GlobalHookKeyUp;
        }

        private void InputTextBox_TextChanged(Object Sender, EventArgs E)
        {
            if (!UpdatingInputTextBox)
                InputTextBox.Text = String.Empty;
            else
                UpdatingInputTextBox = false;
        }

        private void DeleteNodeButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.DeleteNode(GetSelectionIndicies());
        }

        private void ClearInputsButton_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.ClearNodes();
        }

        private void AddInputToolStripMenuItem_Click(Object Sender, EventArgs E)
        {

            InputCorrelatorPresenter.AddInput(GetSelectionIndicies());
        }

        private void AddLogicalORToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddOR(GetSelectionIndicies());
        }

        private void AddLogicalANDToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddAND(GetSelectionIndicies());
        }

        private void AddLogicalNOTToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.AddNOT(GetSelectionIndicies());
        }

        private void InputContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            if (InputTreeView.SelectedNode == null)
                E.Cancel = true;
        }

        private void DeleteToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            InputCorrelatorPresenter.DeleteNode(GetSelectionIndicies());
        }

        private void GUILabelerInputCorrelator_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        #endregion
        
    } // End class

} // End namespace