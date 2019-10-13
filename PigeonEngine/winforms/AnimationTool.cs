using System;
using System.Drawing;
using System.Windows.Forms;
using pigeon.gfx.drawable.sprite;

namespace pigeon.winforms {
    public partial class AnimationTool : Form {
        private Sprite selectedSprite;

        public AnimationTool() {
            InitializeComponent();
        }

        private void AnimationTool_Load(object sender, EventArgs e) {
            Pigeon.Console.ExecuteCommand("palette off");
            Pigeon.Console.ExecuteCommand("lcd false");
            Pigeon.Console.ExecuteCommand("sandbox");

            foreach (var spriteName in Sprite.allSprites.Keys) {
                allSpritesComboBox.Items.Add(spriteName);
            }

            allSpritesComboBox.SelectedIndex = 0;
        }

        private void AllSpritesComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            string spriteName = (string) allSpritesComboBox.SelectedItem;
            populateAnimationList(spriteName);
        }

        private void allAnimationsComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            updatePreview();
        }

        private void populateAnimationList(string spriteName) {
            selectedSprite = Sprite.allSprites[spriteName];

            allAnimationsComboBox.Items.Clear();
            foreach (var animation in selectedSprite.Animations.Keys) {
                allAnimationsComboBox.Items.Add(animation);
            }

            allAnimationsComboBox.SelectedIndex = 0;
        }

        private void backgroundColorButton_Click(object sender, EventArgs e) {
            DialogResult dialogResult = backgroundColorDialog.ShowDialog();

            if (dialogResult == DialogResult.OK) {
                Color winColor = backgroundColorDialog.Color;
                Pigeon.World.BackgroundColor = new Microsoft.Xna.Framework.Color(winColor.R, winColor.G, winColor.B);
            }
        }

        private void previewEnabledCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (previewEnabledCheckBox.Checked) {
                updatePreview();
                centerPreviewCheckBox.Enabled = true;
                loopCheckBox.Enabled = true;
            } else if (!previewEnabledCheckBox.Checked) {
                centerPreviewCheckBox.Enabled = false;
                loopCheckBox.Enabled = false;
                Pigeon.World.DeleteObjSafe("ANIM_TEST");
            }
        }

        private void loopCheckBox_CheckedChanged(object sender, EventArgs e) {
            updatePreview();
        }

        private void centerPreviewCheckBox_CheckedChanged(object sender, EventArgs e) {
            updatePreview();
        }

        private void updatePreview() {
            if (Pigeon.nextWorld != null) {
                Pigeon.nextWorld.AddTask(.25f, this.createTestanim);
            } else {
                createTestanim();
            }
        }

        private void createTestanim() {
            SpriteDebugger.CreateTestAnim((string) allSpritesComboBox.SelectedItem, (string) allAnimationsComboBox.SelectedItem, centerPreviewCheckBox.Checked, loopCheckBox.Checked);
        }
    }
}
