using SiegeUI;
using SiegeUI.Controls;
using SiegeUI.Drawing;

Controller.Init();

Window mainWindow = new Window(null!, "SiegeUITester", new Rectangle(64, 64, 1280, 720), Window.WindowFlags.Sizeable);

Panel panel = new Panel(mainWindow)
{
    Bounds = new Rectangle(32, 32, 256, 160)
};

mainWindow.Children.Add("panel1", panel);

Label label = new Label(panel)
{
    Text = "Hello world",
    Bounds = new Rectangle(32, 32, 0, 0),
    Shadow = true,
    Docking = Control.DockingMode.Top,
    Align = Text.TextAlign.Middle
};
panel.Children.Add("label1", label);

Button button = new Button(panel)
{
    Text = "Button",
    Bounds = new Rectangle(32, 64, 58, 24),
    TextShadow = false,
    Docking = Control.DockingMode.Bottom
};
panel.Children.Add("button1", button);

Controller.Windows.Add(mainWindow);

for (; ;)
{
    Controller.Update();
}