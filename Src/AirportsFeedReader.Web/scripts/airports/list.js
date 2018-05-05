$(() => {
    let CountryModel = function (model) {
        let self = this;
        
        self.Name = "";
        self.CountryCode = "";
        self.Region = "";

        if (model) {
            self.Name = model.Name;
            self.CountryCode = model.CountryCode;
            self.Region = model.Region;
        }
    }

    let AirportModel = function (model) {
        let self = this;

        self.Continent = "";
        self.Iata = "";
        self.Country = "";
        self.Latitude = "";
        self.Longitude = "";
        self.Name = "";
        self.Size = "";
        self.Status = "";
        self.Type = "";

        if (model) {
            self.Continent = model.Continent;
            self.Iata = model.Iata;
            self.Country = model.Iso;
            self.Latitude = model.Latitude;
            self.Longitude = model.Longitude;
            self.Name = model.Name;
            self.Size = model.Size;
            self.Status = model.Status;
            self.Type = model.Type;

        }
    }

    let AirportsViewModel = function () {
        let self = this;

        self.airports = ko.observableArray();
        self.countries = ko.observableArray();

        self.countryCode = ko.observable("");

        self.selectedCountryCode = ko.computed({
            read: () => {
                return self.countryCode();
            }, write: (value) => {
                self.countryCode(value);
                loadGridData();
            }
        }, self);

        self.filteredAirports = ko.pureComputed(() => {
            let isoCode = self.countryCode();

            if (!isoCode) {
                return self.airports();
            }

            let filtered = ko.utils.arrayFilter(self.airports(), function (airport) {
                return airport.Country === isoCode;
            });

            return filtered;

        }, self);

        self.init = () => {
            let airportsJson = self.getApiData("/Airports/GetAirportsList");
            let countriesJson = self.getApiData("/Countries/GetCountriesList");

            airportsJson.forEach((airport, index) => {
                self.airports().push(new AirportModel(airport));
            })
            countriesJson.forEach((country, index) => {
                self.countries().push(new CountryModel(country));
            })
        }

        self.getApiData = (apiUrl, filter) => {
            var data = $.ajax({
                url: apiUrl,
                data: filter,
                dataType: "json",
                async: false,
            });

            return JSON.parse(data.responseText);
        }

        self.init();
    }

    let airports = [];
    let countries = [];
    let $grid = $("#airportGrid");

    let loadGridData = () => {
        $grid.jsGrid("openPage", 1).jsGrid("loadData");
    }

    let initializeGrid = () => {
        return new Promise((resolve, reject) => {
            let $jsGrid = $grid.jsGrid({
                width: "100%",

                filtering: false,
                sorting: false,
                autoload: false,
                paging: true,
                pageLoading: true,
                pageSize: 25,
                pageIndex: 1,

                data1: airports,
                controller: {
                    loadData: function (filter) {
                        var startIndex = (filter.pageIndex - 1) * filter.pageSize;
                        let airports = viewModel.filteredAirports();

                        data = {
                            data: airports.slice(startIndex, startIndex + filter.pageSize),
                            itemsCount: airports.length
                        };

                        return data;
                    }
                },
                fields: [
                    { name: "Name", title: "Airport", type: "text", width: 150 },
                    { name: "Iata", title: "Iata Code", type: "text", width: 50 },
                    { name: "Country", type: "select", items: viewModel.countries(), valueField: "CountryCode", textField: "Name" },
                    { name: "Size", type: "text", width: 50 },
                ]
            });

            resolve($jsGrid);
        });
    }

    let viewModel = new AirportsViewModel();
    ko.applyBindings(viewModel, $("#airports").get(0));

    initializeGrid().then(($grid) => {
        loadGridData();
    });
});