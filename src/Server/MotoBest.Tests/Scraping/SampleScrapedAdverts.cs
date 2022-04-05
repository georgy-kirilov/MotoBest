using System;

using MotoBest.Common;
using MotoBest.Services.Scraping.Models;

namespace MotoBest.Tests.Scraping;

public static class SampleScrapedAdverts
{
    public static class AutoBg
    {
        public static readonly ScrapedAdvert Test_001 = new()
        {
            RemoteId = "17067064",
            Title = "Opel Mokka 1,7-CDTI-4x4-80000км!!!!! - 19 300 лв.",
            Price = 19300,
            Currency = Currency.Bgn,
            BodyStyle = "Джип",
            Engine = "dizelov",
            Transmission = "Ръчна",
            Kilometrage = 80000,
            Condition = "Употребяван",
            ManufacturedOn = new DateTime(2014, 05, 01),
            HorsePowers = 131,
            Color = "Бордо",
            PopulatedPlace = "гр. Димитровград",
            Brand = "Opel",
            Model = "Mokka",
            Region = "регион Хасково",
            EuroStandard = null,
            Description = "Нов внос от Австрия, без забележки по интириора и екстириора Реални км. пълна сервизна история два ключа Всичко " +
            "по коалата работи Възможен лизинг без първоначална вноска и отговор до 30мин Съдействие в КАТ при регистрация или " +
            "вадене на транзитни номера За повече инфо на посочения тел",
            ImageUrls = new[]
            {
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_JI.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_gd.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_N4.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_16.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_Uj.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_CD.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_iV.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_LQ.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/med/21647687287424570_rU.jpg",
                "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_9C.jpg",
            },
        };

        public static readonly ScrapedAdvert Test_002 = new()
        {
            RemoteId = "19495548",
            Title = "Mercedes-Benz E 320 Mercedes-Benz E320CDI 4matic - 10 000 лв.",
            Price = 10000,
            Currency = Currency.Bgn,
            BodyStyle = "Комби",
            Engine = "dizelov",
            Transmission = "Автоматична",
            Kilometrage = 374856,
            Condition = "Употребяван",
            ManufacturedOn = new DateTime(2006, 10, 01),
            HorsePowers = 224,
            Color = "Черен",
            PopulatedPlace = "гр. Перник",
            Brand = "Mercedes Benz",
            Model = "E 320",
            Region = null,
            EuroStandard = null,
            Description = "Внос от Италия преди 1година първи собственик в България, реални километри.  " +
            "Обслужена и подържана винаги на време.  Перфектна работа на мотор и скорости.  " +
            "Няма належащи ремонти  Колата се кара всеки ден  За повече инфо на тел: 0893598392",
            ImageUrls = new[]
            {
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_Hu.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_xD.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_mk.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_9o.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_Fe.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_h5.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_p4.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_67.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/med/11647548823966877_gx.jpg",
                "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_9F.jpg",
            }
        };

        public static readonly ScrapedAdvert Test_003 = new()
        {
            RemoteId= "26525379",
            Title= "Toyota Rav4 2.2D4D-4x4-6SP-ANDROID-VNOS IT-LIZING-GARANCIQ - 12 500 лв.",
            Price= 12500,
            Currency= Currency.Bgn,
            BodyStyle= "Джип",
            Engine= "dizelov",
            Transmission= "Ръчна",
            Kilometrage= 197000,
            Condition= "Употребяван",
            ManufacturedOn= new DateTime(2006, 12, 01),
            HorsePowers= 136,
            Color="Тъмно сив",
            PopulatedPlace= "гр. Нови пазар",
            Brand= "Toyota",
            Model= "Rav4",
            Region= "регион Шумен",
            EuroStandard= null,
            Description = "СОБСТВЕН ЛИЗИНГ БЕЗ ДОКАЗВАНЕ НА ДОХОД! ! ! ОДОБРЕНИЕ ВЕДНАГА НА 100% ! ! ! ! САМОУЧАСТИЕ ОТ 20 ДО 30% ИЛИ СТАРИЯТ ВИ " +
            "АВТОМОБИЛ КАТО ПЪРВОНАЧАЛНА ВНОСКА! ! ! ! ВСИЧКИ АВТОМОБИЛИ ПРИ НАС МОГАТ ДА БЪДАТ ЗАКУПЕНИ СЪС ШЕСТ МЕСЕЦА ГАРАНЦИЯ ИЛИ 30000км! ! " +
            "! ЛИЗИНГ ПРИ МНОГО ДОБРИ УСЛОВИЯ! ! ! ПРЕДЛАГАМЕ АВТОМОБИЛИ ЗА ЕКСПОРТ! ! ! ИЗВАЖДАНЕ НА ТРАНЗИТНИ НОМЕРА ЗА ЕКСПОРТ! ! ! ОСИГУРЕН " +
            "ТРАНСПОРТ НА АВТОМОБИЛЪТ ДО ВСЕКИ ГРАНИЧЕН ПУНКТ В ДЪРЖАВАТА! ! ! . . . Автомобилът е внос от северна Италия(Комо) ! ! ! Много добро " +
            "техническо състояние без забележки ! ! ! Без никаква ръжда по купето! ! ! Работещ климатроник! ! ! Мултимедия/навигация \" Android\" " +
            "с 8 инча дисплей! ! ! Два ключа! ! ! камера за задно виждане! ! ! Нови гуми! ! ! Теглич(Сваляем)! ! !   ТОП СЪСТОЯНИЕ, неразличим от " +
            "нов! ! ! Автокъща Пеци предлага на всички свои клиенти възможността, всички автомобили които искат да закупят, да бъдат тествани и " +
            "прегледани в сервиз пред тях, относно техническото им състояние! ! ! ! Изготвяме всички необходими документи по регистрация на превозното " +
            "средство. Фактура, договор за покупко-продажба, декларации, екотакса и други! ! ! ! Плащането може да бъде извършвано по банков път! ! ! " +
            "Извършваме и регистрация! ! ! Осигурен транспорт или транзитни табели! ! ! Всички цени за бартер се договарят след оглед на Вашия автомобил" +
            "! ! ! Заповядайте в автокъща Пеци 100% коректност от наша страна! ! ! Автокъща Пеци е повече от 15 години на пазара с автомобили, ще " +
            "намерите и направите правилният избор при покупка на автомобил! ! ! ! Качествени автомобили на много добри цени, придружени с гаранция! " +
            "! ! Гарантирано качество! ! ! ! За повече информация звъннете на посочените телефони! ! ! !",
            ImageUrls = new[]
            {
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_oO.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_94.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_Dx.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_G4.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_1C.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_8C.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_Fr.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_dN.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/med/21643890746724459_E2.jpg",
                "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_xW.jpg",
            }
        };
    }
}
