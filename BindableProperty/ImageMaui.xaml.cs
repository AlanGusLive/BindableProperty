namespace BindableProperty;
using Microsoft.Maui.Controls;
using System.Diagnostics;

public partial class ImageMaui : ContentView
{
    public static readonly BindableProperty SourceProperty =
        BindableProperty.CreateAttached(
            "Source",
            typeof(string),
            typeof(ImageMaui),
            String.Empty,
            BindingMode.TwoWay,
            null,
            OnSourceChanged
            );

    public static readonly BindableProperty RotationMauiProperty =
        BindableProperty.CreateAttached(
            "RotationMaui",
            typeof(string),
            typeof(ImageMaui),
            "0",
            BindingMode.TwoWay,
            null,
            OnRotationChanged
            );

    public ImageMaui()
	{
		InitializeComponent();
	}

    private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ImageMaui Im = bindable as ImageMaui;
        string strSource = newValue as string;

        if (Im != null && !String.IsNullOrWhiteSpace(strSource))
        {
            Im.Source = strSource;
        }
    }

    public string Source
    {
        get { return (string)GetValue(SourceProperty); }
        set
        {
            SetValue(SourceProperty, value);

            Application.Current.Dispatcher.Dispatch(
            () =>
            {
                if (!String.IsNullOrWhiteSpace(Source))
                {
                    UpdateUserControl();
                }
            });
        }
    }

    private void UpdateUserControl()
    {
        // The goal is to avoid to load each time the image from the file but to load it from a cache in memory.
        ImageUserControl.Source = ImageSource.FromFile(Source);
        int _degree = 0;
        if (int.TryParse(RotationMaui, out _degree))
        {
            ImageUserControl.Rotation = _degree;
            Debug.WriteLine("Rotation UpdateUserControl: " + ImageUserControl.Rotation);
        }
        else
        {
            Debug.Assert(false);
        }
    }

    private static void OnRotationChanged(BindableObject bindable, object oldValue, object newValue)
    {

        ImageMaui Im = bindable as ImageMaui;
        string strRotation = newValue as string;

        if (Im != null && string.IsNullOrEmpty(strRotation) == false)
        {
            int _degree = 0;
            if (int.TryParse(strRotation, out _degree))
            {
                Im.Rotation = _degree;
                Debug.WriteLine("Rotation OnRotationChanged: " + Im.Rotation);
            }
            else
            {
                Debug.Assert(false);
            }
        }
        else
        {
            Debug.Assert(false);
        }
    }

    public string RotationMaui
    {
        get { return (string)GetValue(RotationMauiProperty); }
        set
        {
            SetValue(RotationMauiProperty, value);
        }
    }
}