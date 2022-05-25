using SiegeUI;
using SiegeUI.Controls;
using SiegeUI.Drawing;

SiegeUI_Controller.Init();

SiegeUI_Window mainWindow = new SiegeUI_Window("SiegeUITester", new SiegeUI_Rectangle(64, 64, 1280, 720), SiegeUI_Window.WindowFlags.Sizeable);
SiegeUI_Label label = new SiegeUI_Label()
{
    Text = "Hello world",
    Bounds = new SiegeUI_Rectangle(32, 32, 0, 0),
    Shadow = true
};

mainWindow.Controls.Add("label1", label);

SiegeUI_Controller.Windows.Add(mainWindow);

for (; ;)
{
    SiegeUI_Controller.Update();
}