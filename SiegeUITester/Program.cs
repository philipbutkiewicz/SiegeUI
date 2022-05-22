using SiegeUI;
using SiegeUI.Drawing;

SiegeUI_Controller.Init();
SiegeUI_Controller.Windows.Add(new SiegeUI_Window("SiegeUITester", new SiegeUI_Rectangle(64, 64, 1280, 720), SiegeUI_Window.WindowFlags.Sizeable));

for (; ;)
{
    SiegeUI_Controller.Update();
}