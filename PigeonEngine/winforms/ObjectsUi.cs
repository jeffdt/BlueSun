using System;
using System.Collections.Generic;
using System.Windows.Forms;
using pigeon.gameobject;

namespace pigeon.winforms {
    public partial class PigeonUi {
        private GameObject currentObj;

        private void populateObjectTree() {
            enableObjectDetails(false);

            objectTree.Nodes.Clear();
            var allObjects = Pigeon.World.RootObj.GetChildren();
            populateObjectTreeRecursive(objectTree.Nodes, allObjects);
        }

        private void enableObjectDetails(bool enabled) {
            objectDetailsGroupBox.Enabled = enabled;
            objectDetailsRefreshButton.Visible = enabled;
        }

        private void populateObjectTreeRecursive(TreeNodeCollection nodeCollection, IEnumerable<GameObject> objects) {
            if (objects == null) {
                return;
            }

            foreach (var obj in objects) {
                var treeNode = nodeCollection.Add(obj.Name);
                var children = obj.GetChildren();
                populateObjectTreeRecursive(treeNode.Nodes, children);
            }
        }

        private void refreshObjectDetails(GameObject obj) {
            if (obj == null) {
                enableObjectDetails(false);
                return;
            }

            enableObjectDetails(true);

            objectNameText.Text = obj.Name;
            objectChildrenCountText.Text = obj.GetChildrenCount().ToString();

            objectFlipXCheckBox.Checked = obj.IsFlippedX();
            objectFlipYCheckBox.Checked = obj.IsFlippedY();

            objectUpdateEnabledCheckbox.Checked = obj.UpdateEnabled;
            objectDrawEnabledCheckbox.Checked = obj.DrawEnabled;

            objectInheritLayerCheckBox.Checked = obj.LayerInheritanceEnabled;
            objectVarianceCheckBox.Checked = obj.LayerSortingVarianceEnabled;

            refreshObjectLayerInfo(obj);

            localPositionXText.Text = obj.LocalPosition.X.ToString();
            localPositionYText.Text = obj.LocalPosition.Y.ToString();
            worldPositionXText.Text = obj.WorldPosition.X.ToString();
            worldPositionYText.Text = obj.WorldPosition.Y.ToString();
            drawPositionCheckbox.Checked = obj.DrawPosition;

            populateComponentsList(obj);
        }

        private void refreshObjectLayerInfo(GameObject obj) {
            const string layerFormat = "F6";
            objectLayerText.Text = obj.Layer.ToString(layerFormat);
            objectParentLayerText.Text = obj.Parent.DrawLayer.ToString(layerFormat);
            objectParentLayerText.Enabled = obj.LayerInheritanceEnabled;
            objectLayerVarianceText.Text = obj.sortVariance.ToString(layerFormat);
            objectLayerVarianceText.Enabled = obj.LayerSortingVarianceEnabled;
            objectDrawLayerText.Text = obj.DrawLayer.ToString(layerFormat);
        }

        private static string convertObjectPath(string fullPath) {
            return fullPath.Replace(@"\", @".");
        }

        private void populateComponentsList(GameObject obj) {
            componentsList.Items.Clear();

            if (obj.components != null) {
                foreach (var component in obj.components) {
                    componentsList.Items.Add(component.GetType().Name);
                }
            }

            componentCountText.Text = obj.ComponentCount.ToString();
        }

        #region object events
        #region object list
        private void objectListRefresh_Click(object sender, EventArgs e) {
            populateObjectTree();
        }

        private void objectTree_AfterSelect(object sender, TreeViewEventArgs e) {
            currentObj = Pigeon.World.FindObj(convertObjectPath(e.Node.FullPath));
            refreshObjectDetails(currentObj);
        }
        #endregion object list

        #region flipping
        private void objectFlipXCheckBox_CheckedChanged(object sender, EventArgs e) {
            currentObj.SetFlipX(objectFlipXCheckBox.Checked);
        }

        private void objectFlipYCheckBox_CheckedChanged(object sender, EventArgs e) {
            currentObj.SetFlipY(objectFlipYCheckBox.Checked);
        }
        #endregion flipping

        #region control
        private void objectUpdateEnabledCheckbox_CheckedChanged(object sender, EventArgs e) {
            currentObj.UpdateEnabled = objectUpdateEnabledCheckbox.Checked;
        }

        private void objectDrawEnabledCheckbox_CheckedChanged(object sender, EventArgs e) {
            currentObj.DrawEnabled = objectDrawEnabledCheckbox.Checked;
        }
        #endregion control

        #region layer
        private void objectInheritLayerCheckBox_CheckedChanged(object sender, EventArgs e) {
            currentObj.LayerInheritanceEnabled = objectInheritLayerCheckBox.Checked;
            refreshObjectLayerInfo(currentObj);
        }

        private void objectVarianceCheckBox_CheckedChanged(object sender, EventArgs e) {
            currentObj.LayerSortingVarianceEnabled = objectVarianceCheckBox.Checked;
            refreshObjectLayerInfo(currentObj);
        }

        private void objectDetailsRefreshButton_Click(object sender, EventArgs e) {
            refreshObjectDetails(currentObj);
        }
        #endregion layer

        #region position
        private void drawPositionCheckbox_CheckedChanged(object sender, EventArgs e) {
            currentObj.DrawPosition = drawPositionCheckbox.Checked;
        }
        #endregion position

        #endregion object events

    }
}
