﻿using System.Collections.ObjectModel;

namespace TD2.Models
{
    public static class PlaceList
    {
        public static ObservableCollection<Place> Places { get; }

        static PlaceList()
        {
            Places = new ObservableCollection<Place>();
        }

        public static void AddPlace(Place place)
        {
            Places.Add(place);
        }
    }
}