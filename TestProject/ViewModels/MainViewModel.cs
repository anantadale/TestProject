using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Linq;
using TestProject.Models;
namespace TestProject.ViewModels;

// ViewModel implementing MVVM to control UI logic and API data
public class MainViewModel : INotifyPropertyChanged
{
    private ObservableCollection<User> _allUsers;
    private ObservableCollection<User> _filteredUsers;
    private ObservableCollection<string> _cities;
    private string _selectedCity;

    // Collection bound to the ListView
    public ObservableCollection<User> FilteredUsers
    {
        get => _filteredUsers;
        set { _filteredUsers = value; OnPropertyChanged(); }
    }

    // Collection bound to the ComboBox
    public ObservableCollection<string> Cities
    {
        get => _cities;
        set { _cities = value; OnPropertyChanged(); }
    }

    // Property bound to selected item in ComboBox
    public string SelectedCity
    {
        get => _selectedCity;
        set
        {
            _selectedCity = value;
            OnPropertyChanged();
            ApplyFilter();
        }
    }
    // Constructor that triggers data load from API
    public MainViewModel()
    {
        LoadData();
    }

    // Fetches user data from API and initializes dropdown and list
    private async void LoadData()
    {
        var httpClient = new HttpClient();
        var json = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");
        _allUsers = JsonSerializer.Deserialize<ObservableCollection<User>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Cities = new ObservableCollection<string>(_allUsers.Select(u => u.Address.City).Distinct());
        FilteredUsers = new ObservableCollection<User>(_allUsers);
    }

    // Applies filtering logic to only show users from selected city
    private void ApplyFilter()
    {
        if (string.IsNullOrEmpty(SelectedCity))
            FilteredUsers = new ObservableCollection<User>(_allUsers);
        else
            FilteredUsers = new ObservableCollection<User>(_allUsers.Where(u => u.Address.City == SelectedCity));
    }
    // INotifyPropertyChanged Implementation to update UI
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

