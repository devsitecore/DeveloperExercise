$(() => {
    Number.prototype.formatNumber = function (c, d, t) {
        var n = this,
        c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;
        return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    };

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

    let DistanceModel = function (distance, unit, apiUrl) {
        let self = this;

        self.Distance = ko.observable(distance);
        self.Unit = ko.observable(unit);
        self.ApiUrl = ko.observable(apiUrl);

        self.DistanceDisplay = ko.pureComputed(() => {
            let distance = self.Distance().formatNumber();
            let unit = self.Unit();

            if (unit) {
                if (distance !== 1) {
                    unit = unit + "s";
                }

                return `${distance} ${unit}`;
            }

            return "";
        }, self);
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

    let AirportsViewModel = function (loadCountries, callBack) {
        let self = this;

        self.airports = ko.observableArray();
        self.countries = ko.observableArray();

        self.countryCode = ko.observable("");

        self.sourceAirport = ko.observable("");
        self.destinationAirport = ko.observable("");

        self.distanceModel = ko.observable(new DistanceModel(0, "", ""));

        self.calculateDistance = (event) => {
            let apiURL = `/airports/calculatedistance?source=${self.sourceAirport()}&destination=${self.destinationAirport()}`;

            let distanceJson = self.getApiData(apiURL);
            let model = self.distanceModel();
            model.Distance(distanceJson.Distance);
            model.Unit(distanceJson.Unit);
            model.ApiUrl(apiURL);
        };

        self.selectedCountryCode = ko.computed({
            read: () => {
                return self.countryCode();
            }, write: (value) => {
                self.countryCode(value);

                if (callBack) {
                    callBack();
                }
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
            if (loadCountries) {
                let countriesJson = self.getApiData("/Countries/GetCountriesList");

                countriesJson.forEach((country, index) => {
                    self.countries.push(new CountryModel(country));
                });
            }

            let airportsJson = self.getApiData("/Airports/GetAirportsList")
            airportsJson.forEach((airport, index) => {
                self.airports.push(new AirportModel(airport));
            });
        }

        self.getApiData = (apiUrl, filter) => {
            //return new Promise((resolve, reject) => {
                var data = $.ajax({
                    url: apiUrl,
                    data: filter,
                    dataType: "json",
                    async: false,
                });

                let fromFeed = data.getResponseHeader("from-feed");

                if (fromFeed) {
                    console.log(`${apiUrl} returned the header "from-feed"="${fromFeed}"`);
                }
                return JSON.parse(data.responseText);
                //resolve(JSON.parse(data.responseText));
            //});
        }

        self.init();
    }

    let $grid = $("#airportGrid");

    let loadGridData = () => {
        $grid.jsGrid("openPage", 1).jsGrid("loadData");
    }

    let initializeGrid = (viewModel) => {
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

    let $airportsContainer = $("#airports");
    let $airportsDistanceContainer = $("#airportsDistance");
    
    if ($airportsContainer.length > 0) {
        let $viewModel = new AirportsViewModel(true, () => { loadGridData();});
        ko.applyBindings($viewModel, $airportsContainer.get(0));

        initializeGrid($viewModel).then(($grid) => {
            loadGridData();
        });
    }
    else if ($airportsDistanceContainer.length > 0) {
        let $viewModel = new AirportsViewModel();
        ko.applyBindings($viewModel, $airportsDistanceContainer.get(0));
    }
});