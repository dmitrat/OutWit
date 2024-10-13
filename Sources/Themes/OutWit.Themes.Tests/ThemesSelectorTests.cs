namespace OutWit.Themes.Tests
{
    public class ThemesSelectorTests
    {
        [Test]
        public void InitializationTest()
        {
            int themesEventCounter = 0;

            Selector.Get.Reload(null);
            var container = Selector.Get.ThemeContainer;
            container.CurrentThemeChanged += t => { themesEventCounter++; };

            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Dark"));

            Assert.That(container.AllThemes.Count(), Is.EqualTo(2));

            Selector.Get.ThemeContainer.SetCurrentTheme("Light");
            Assert.That(themesEventCounter, Is.EqualTo(1));
            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Light"));

            Selector.Get.ThemeContainer.SetCurrentTheme("Dark"); Assert.That(themesEventCounter, Is.EqualTo(2));
            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Dark"));

            Selector.Get.ThemeContainer.SetCurrentTheme("Light");
            Assert.That(themesEventCounter, Is.EqualTo(3));
            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Light"));

            Selector.Get.Reload(null);

            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Dark"));

            Assert.That(container.AllThemes.Count(), Is.EqualTo(2));

            Selector.Get.ThemeContainer.SetCurrentTheme("Light");
            Assert.That(themesEventCounter, Is.EqualTo(4));
            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Light"));

            Selector.Get.ThemeContainer.SetCurrentTheme("Dark");
            Assert.That(themesEventCounter, Is.EqualTo(5));
            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Dark"));

            Selector.Get.ThemeContainer.SetCurrentTheme("Light");
            Assert.That(themesEventCounter, Is.EqualTo(6));
            Assert.That(container.Current, Is.Not.Null);
            Assert.That(container.Current.Key, Is.EqualTo("Light"));
        }
    }
}