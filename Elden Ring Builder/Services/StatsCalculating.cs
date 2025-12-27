using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Elden_Ring_Builder.Services
{
    public class StatsCalculating
    {
        private class CharacterClass
        {
            public int BaseLevel { get; set; }
            public int BaseStatsSum { get; set; }

        }
        private readonly Dictionary<string, CharacterClass> _classes =
            new Dictionary<string, CharacterClass>
            {
                { "Warrior",     new CharacterClass { BaseLevel = 8,  BaseStatsSum = 86 } },
                { "Vagabond",    new CharacterClass { BaseLevel = 9,  BaseStatsSum = 88 } },
                { "Hero",        new CharacterClass { BaseLevel = 7,  BaseStatsSum = 86 } },
                { "Bandit",      new CharacterClass { BaseLevel = 5,  BaseStatsSum = 83 } },
                { "Astrologer",  new CharacterClass { BaseLevel = 6,  BaseStatsSum = 86 } },
                { "Prophet",     new CharacterClass { BaseLevel = 7,  BaseStatsSum = 85 } },
                { "Samurai",     new CharacterClass { BaseLevel = 9,  BaseStatsSum = 88 } },
                { "Prisoner",    new CharacterClass { BaseLevel = 9,  BaseStatsSum = 85 } },
                { "Confessor",   new CharacterClass { BaseLevel = 10, BaseStatsSum = 89 } },
                { "Wretch",      new CharacterClass { BaseLevel = 1,  BaseStatsSum = 80 } },
            };

        public void CalculateLevel(ComboBox classComboBox, Slider vigorSlider, Slider mindSlider, Slider enduranceSlider, Slider strenghtSlider, Slider dexteritySlider, Slider intelligenceSlider, Slider faithSlider, Slider arcaneSlider, TextBlock estimatedLevelTextBlock)
        {
            if (classComboBox.SelectedItem == null)
                return;

            var selectedItem = classComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            string? className = selectedItem.Content.ToString();

            if (!_classes.TryGetValue(className, out var characterClass))
                return;

            int totalStats =
                (int)vigorSlider.Value +
                (int)mindSlider.Value +
                (int)enduranceSlider.Value +
                (int)strenghtSlider.Value +
                (int)dexteritySlider.Value +
                (int)intelligenceSlider.Value +
                (int)faithSlider.Value +
                (int)arcaneSlider.Value;

            int level = characterClass.BaseLevel +
                        (totalStats - characterClass.BaseStatsSum);

            if (level < characterClass.BaseLevel)
                level = characterClass.BaseLevel;

            estimatedLevelTextBlock.Text = level.ToString();
        }

        private readonly Dictionary<string, string> _classImages = new()
        {
            { "Warrior", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/warrior_class_elden_ring_wiki_guide_200px.png" },
            { "Samurai", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/samurai_class_elden_ring_wiki_guide_200px.png" },
            { "Vagabond", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/vagabond_class_elden_ring_wiki_guide_200px.png" },
            { "Hero", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/hero_class_elden_ring_wiki_guide_200px.png" },
            { "Bandit", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/bandit_class_elden_ring_wiki_guide_200px.png" },
            { "Astrologer", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/astrologer_class_elden_ring_wiki_guide_200px.png" },
            { "Prophet", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/prophet_class_elden_ring_wiki_guide_200px.png" },
            { "Prisoner", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/prisoner_class_elden_ring_wiki_guide_200px.png" },
            { "Confessor", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/confessor_class_elden_ring_wiki_guide_200px.png" },
            { "Wretch", "https://eldenring.wiki.fextralife.com/file/Elden-Ring/wretch_class_elden_ring_wiki_guide_200px.png" },
        };
        public void ClassComboBox_Image(ComboBox classComboBox, Image class_image)
        {
            if (classComboBox.SelectedItem is not ComboBoxItem item)
                return;

            if (!_classImages.TryGetValue(item.Tag.ToString(), out var url))
                return;

            class_image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
        }
    }
}
