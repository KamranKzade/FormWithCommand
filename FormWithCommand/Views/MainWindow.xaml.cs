using FormWithCommand.Commands;
using FormWithCommand.Models;
using FormWithCommand.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace FormWithCommand.Views;



public partial class MainWindow : Window, INotifyPropertyChanged
{
    private List<Human> humen;

    public List<Human> Humen
    {
        get { return humen; }
        set { humen = value; OnPropertyChanged(); }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null!)
    {
        PropertyChangedEventHandler handler = PropertyChanged!;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }

    public RelayCommand FirstNameEnterCommand { get; set; }
    public RelayCommand LastNameEnterCommand { get; set; }
    public RelayCommand PhoneEnterCommand { get; set; }
    public RelayCommand EmailEnterCommand { get; set; }
    public RelayCommand SubmitButtonCommand { get; set; }


    Regex validatePhoneNumberRegex = new("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
    Regex validateEmailRegex = new("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");


    public MainWindow()
    {
        InitializeComponent();

        Humen = new List<Human>();

        Humen = FakeRepo.GetHumen();

        DataContext = this;

        FirstNameEnterCommand = new RelayCommand((o) =>
        {
            var mystackPanel = o as StackPanel;
            var firtsNameTextBox = mystackPanel!.Children[1] as TextBox;
            var button = (mystackPanel.Children[8] as Border)!.Child as Button;

            if (string.IsNullOrWhiteSpace(firtsNameTextBox!.Text) || firtsNameTextBox.Text.Length < 4 || !char.IsUpper(firtsNameTextBox!.Text[0]))
            {
                firtsNameTextBox!.BorderBrush = new SolidColorBrush(Colors.Red);
                firtsNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);
                firtsNameTextBox.Focus();
                button!.IsEnabled = false;
            }
            else
            {
                firtsNameTextBox!.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                firtsNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                var textBox2 = mystackPanel.Children[3] as TextBox;
                textBox2!.Focus();
                button!.IsEnabled = true;
            }

        });


        LastNameEnterCommand = new RelayCommand((o) =>
        {
            var stackPanel = o as StackPanel;
            var lastNameTextBox = stackPanel!.Children[3] as TextBox;
            var button = (stackPanel!.Children[8] as Border)!.Child as Button;

            if (string.IsNullOrWhiteSpace(lastNameTextBox!.Text) || lastNameTextBox.Text.Length < 4 || !char.IsUpper(lastNameTextBox.Text[0]))
            {
                lastNameTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                lastNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);
                lastNameTextBox.Focus();
                button!.IsEnabled = false;
            }
            else
            {
                lastNameTextBox.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                lastNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                var PhonetextBox = stackPanel.Children[5] as TextBox;
                PhonetextBox!.Focus();
                button!.IsEnabled = true;
            }
        });


        PhoneEnterCommand = new RelayCommand((o) =>
        {
            var stackPanel = o as StackPanel;
            var phoneTextBox = stackPanel!.Children[5] as TextBox;
            var button = (stackPanel!.Children[8] as Border)!.Child as Button;
            if (validatePhoneNumberRegex.IsMatch(phoneTextBox!.Text) &&
            (phoneTextBox.Text.StartsWith("+994 51") || phoneTextBox.Text.StartsWith("+994 50") || phoneTextBox.Text.StartsWith("+994 55")
            || phoneTextBox.Text.StartsWith("+994 70") || phoneTextBox.Text.StartsWith("+994 77")))
            {
                phoneTextBox.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                phoneTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                var emailTextbox = stackPanel.Children[7] as TextBox;
                emailTextbox!.Focus();
                button!.IsEnabled = true;
            }
            else
            {
                phoneTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                phoneTextBox.BorderThickness = new Thickness(0, 0, 0, 5);
                phoneTextBox.Focus();
                button!.IsEnabled = false;
            }
        });

        EmailEnterCommand = new RelayCommand((o) =>
        {
            var stackPanel = o as StackPanel;
            var EmailTextBox = stackPanel!.Children[7] as TextBox;
            var border = (stackPanel!.Children[8] as Border);
            var button = border!.Child as Button;

            if (validateEmailRegex.IsMatch(EmailTextBox!.Text)
            && (EmailTextBox.Text.EndsWith("@gmail.com") || EmailTextBox.Text.EndsWith("@mail.ru") || EmailTextBox.Text.EndsWith("@yahoo.com")))
            {
                EmailTextBox.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                EmailTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                button!.Foreground = new SolidColorBrush(Colors.SpringGreen);
                button!.Focus();
                button!.IsEnabled = true;
            }
            else
            {
                EmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                EmailTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                button!.Foreground = new SolidColorBrush(Colors.Red);
                EmailTextBox.Focus();
                button!.IsEnabled = false;
            }
        });


        SubmitButtonCommand = new RelayCommand((o) =>
        {
            var stackPanel = o as StackPanel;
            var firstName = stackPanel!.Children[1] as TextBox;
            var lastName = stackPanel!.Children[3] as TextBox;
            var Phone = stackPanel!.Children[5] as TextBox;
            var Email = stackPanel!.Children[7] as TextBox;
            var button = (stackPanel!.Children[8] as Border)!.Child as Button;

            button!.IsEnabled = true;

            foreach (var human in Humen)
            {
                if (human.FirstName == firstName!.Text && lastName!.Text == human.LastName &&
                Phone!.Text == human.PhoneNumber && Email!.Text == human.EmailAddress)
                {
                    MessageBox.Show($"Welcome {firstName.Text} {lastName.Text}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else if (firstName!.Text == string.Empty)
                {
                    MessageBox.Show($"Enter FirstName ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else if (lastName!.Text == string.Empty)
                {
                    MessageBox.Show($"Enter LastName ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else if (Phone!.Text == string.Empty)
                {
                    MessageBox.Show($"Enter Phone Number ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else if (Email!.Text == string.Empty)
                {
                    MessageBox.Show($"Enter Email address ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
            MessageBox.Show("Belə bir Şəxs tapılmadı !!! ", "Information", MessageBoxButton.OK, MessageBoxImage.Error);


        });
    }


}