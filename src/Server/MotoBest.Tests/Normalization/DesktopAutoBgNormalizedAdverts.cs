using System;

using MotoBest.Data.Models;
using MotoBest.Data.Seeding.Constants;

using MotoBest.Services.Normalization;

namespace MotoBest.Tests.Normalizing;

public static class DesktopAutoBgNormalizedAdverts
{
    public static readonly NormalizedAdvert Test_001 = new()
    {
        RemoteId = "17067064",
        RemoteSlug = "opel-mokka-1-7-cdti-4x4-80000km",
        Title = "Opel Mokka 1,7-CDTI-4x4-80000км!!!!!",
        Description = "Нов внос от Австрия, без забележки по интириора и екстириора Реални км. пълна сервизна история два ключа Всичко " +
        "по коалата работи Възможен лизинг без първоначална вноска и отговор до 30мин Съдействие в КАТ при регистрация или " +
        "вадене на транзитни номера За повече инфо на посочения тел",
        PriceInBgn = 19300,
        BodyStyle = BodyStyleNames.Jeep,
        Engine = EngineNames.Diesel,
        Transmission = TransmissionNames.Manual,
        MileageInKm = 80000,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2014, 5, 1),
        PowerInHp = 131,
        Color = ColorNames.Bordo,
        PopulatedPlace = "Димитровград",
        Brand = BrandNames.Opel,
        Model = "Mokka",
        Region = RegionNames.Haskovo,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_JI.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_gd.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_N4.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_16.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_Uj.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_CD.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_iV.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_LQ.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_rU.jpg",
            "//cdn2.focus.bg/mobile/photosorg/570/2/big/21647687287424570_9C.jpg",
        },
    };

    public static readonly NormalizedAdvert Test_002 = new()
    {
        RemoteId = "19495548",
        RemoteSlug = "mercedes-benz-e-320-mercedes-benz-e320cdi-4matic",
        Title = "Mercedes-Benz E 320 Mercedes-Benz E320CDI 4matic",
        Description = "Внос от Италия преди 1година първи собственик в България, реални километри. " +
        "Обслужена и подържана винаги на време. Перфектна работа на мотор и скорости. " +
        "Няма належащи ремонти Колата се кара всеки ден За повече инфо на тел: 0893598392",
        PriceInBgn = 10000,
        BodyStyle = BodyStyleNames.StationWagon,
        Engine = EngineNames.Diesel,
        Transmission = TransmissionNames.Automatic,
        MileageInKm = 374856,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2006, 10, 1),
        PowerInHp = 224,
        Color = ColorNames.Black,
        PopulatedPlace = "Перник",
        Brand = BrandNames.MercedesBenz,
        Model = "E 320",
        Region = null,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_Hu.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_xD.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_mk.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_9o.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_Fe.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_h5.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_p4.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_67.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_gx.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/877/1/big/11647548823966877_9F.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_003 = new()
    {
        RemoteId = "26525379",
        RemoteSlug = "toyota-rav4-2-2d4d-4x4-6sp-android-vnos-it-lizing-garanciq",
        Title = "Toyota Rav4 2.2D4D-4x4-6SP-ANDROID-VNOS IT-LIZING-GARANCIQ",
        Description = "СОБСТВЕН ЛИЗИНГ БЕЗ ДОКАЗВАНЕ НА ДОХОД! ! ! ОДОБРЕНИЕ ВЕДНАГА НА 100% ! ! ! ! САМОУЧАСТИЕ ОТ 20 ДО 30% ИЛИ СТАРИЯТ ВИ " +
        "АВТОМОБИЛ КАТО ПЪРВОНАЧАЛНА ВНОСКА! ! ! ! ВСИЧКИ АВТОМОБИЛИ ПРИ НАС МОГАТ ДА БЪДАТ ЗАКУПЕНИ СЪС ШЕСТ МЕСЕЦА ГАРАНЦИЯ ИЛИ 30000км! ! " +
        "! ЛИЗИНГ ПРИ МНОГО ДОБРИ УСЛОВИЯ! ! ! ПРЕДЛАГАМЕ АВТОМОБИЛИ ЗА ЕКСПОРТ! ! ! ИЗВАЖДАНЕ НА ТРАНЗИТНИ НОМЕРА ЗА ЕКСПОРТ! ! ! ОСИГУРЕН " +
        "ТРАНСПОРТ НА АВТОМОБИЛЪТ ДО ВСЕКИ ГРАНИЧЕН ПУНКТ В ДЪРЖАВАТА! ! ! . . . Автомобилът е внос от северна Италия(Комо) ! ! ! Много добро " +
        "техническо състояние без забележки ! ! ! Без никаква ръжда по купето! ! ! Работещ климатроник! ! ! Мултимедия/навигация \" Android\" " +
        "с 8 инча дисплей! ! ! Два ключа! ! ! камера за задно виждане! ! ! Нови гуми! ! ! Теглич(Сваляем)! ! ! ТОП СЪСТОЯНИЕ, неразличим от " +
        "нов! ! ! Автокъща Пеци предлага на всички свои клиенти възможността, всички автомобили които искат да закупят, да бъдат тествани и " +
        "прегледани в сервиз пред тях, относно техническото им състояние! ! ! ! Изготвяме всички необходими документи по регистрация на превозното " +
        "средство. Фактура, договор за покупко-продажба, декларации, екотакса и други! ! ! ! Плащането може да бъде извършвано по банков път! ! ! " +
        "Извършваме и регистрация! ! ! Осигурен транспорт или транзитни табели! ! ! Всички цени за бартер се договарят след оглед на Вашия автомобил" +
        "! ! ! Заповядайте в автокъща Пеци 100% коректност от наша страна! ! ! Автокъща Пеци е повече от 15 години на пазара с автомобили, ще " +
        "намерите и направите правилният избор при покупка на автомобил! ! ! ! Качествени автомобили на много добри цени, придружени с гаранция! " +
        "! ! Гарантирано качество! ! ! ! За повече информация звъннете на посочените телефони! ! ! !",
        PriceInBgn = 12500,
        BodyStyle = BodyStyleNames.Jeep,
        Engine = EngineNames.Diesel,
        Transmission = TransmissionNames.Manual,
        MileageInKm = 197000,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2006, 12, 1),
        PowerInHp = 136,
        Color = ColorNames.DarkGray,
        PopulatedPlace = "Нови пазар",
        Brand = BrandNames.Toyota,
        Model = "Rav4",
        Region = RegionNames.Shumen,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_oO.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_94.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_Dx.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_G4.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_1C.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_8C.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_Fr.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_dN.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_E2.jpg",
            "//mobistatic4.focus.bg/mobile/photosorg/459/2/big/21643890746724459_xW.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_004 = new()
    {
        RemoteId = "31490804",
        RemoteSlug = "opel-corsa-1-0i",
        Title = "Opel Corsa 1.0I",
        Description = null,
        PriceInBgn = 4200,
        BodyStyle = BodyStyleNames.Hatchback,
        Engine = EngineNames.Gasoline,
        Transmission = TransmissionNames.Manual,
        MileageInKm = 135000,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2007, 8, 1),
        PowerInHp = 60,
        Color = ColorNames.Black,
        PopulatedPlace = "Драгичево",
        Brand = BrandNames.Opel,
        Model = "Corsa",
        Region = RegionNames.Pernik,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.Village,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_4s.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_uj.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_PL.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_3O.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_DX.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_PE.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_Wl.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_Oe.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_96.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/937/1/big/11647691017352937_kE.jpg",
        },
    };

    public static readonly NormalizedAdvert Test_005 = new()
    {
        RemoteId = "32842469",
        RemoteSlug = "hyundai-i20-hot-hatch-1-4-100k-s",
        Title = "Hyundai I20 Hot hatch 1.4, 100к.с.",
        Description = "Автомобилът е в абсолютно оригинален вид, напълно обслужен. Сменени флуиди и всички филтри, комплект накладки, тампони на " +
        "стабилизираща щанга - декември 2021. (Колата не е карана от тогава. ) Платени данъци, каско, гражданска отговорност и преглед валидни до " +
        "декември 2022. Всички екстри се виждат на снимките. Специфично за автомобила е че с двигател 1400куб. см, 16 клапана и мощност от 100к. с. " +
        "Автомобила няма забележки по интериор и екстриор! ! ! Причина за продажбата- покупка на друг автомобил. Огледи се правят след уговорка на " +
        "телефон - 0877090322. Коментари на цената се правят само след оглед на място. Моля да уважаваме парите и автомобилите и да не се пазарим по телефона.",
        PriceInBgn = 16990,
        BodyStyle = BodyStyleNames.Hatchback,
        Engine = EngineNames.Gasoline,
        Transmission = TransmissionNames.Manual,
        MileageInKm = 68100,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2016, 1, 1),
        PowerInHp = 100,
        Color = ColorNames.Graphite,
        PopulatedPlace = "София",
        Brand = BrandNames.Hyundai,
        Model = "I20",
        Region = null,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_hX.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_Fq.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_Ss.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_Q6.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_UH.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_m4.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_Fl.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_NZ.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_GJ.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/101/1/big/11647703904840101_a.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_006 = new()
    {
        RemoteId = "62031689",
        Title = "Lada Niva",
        RemoteSlug = "lada-niva",
        Description = null,
        PriceInBgn = 0,
        BodyStyle = BodyStyleNames.Jeep,
        Engine = EngineNames.Gasoline,
        Transmission = TransmissionNames.Manual,
        MileageInKm = 180000,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(1996, 1, 1),
        PowerInHp = 75,
        Color = ColorNames.Green,
        PopulatedPlace = "Смолян",
        Brand = BrandNames.Lada,
        Model = "Niva",
        Region = null,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//cdn2.focus.bg/mobile/photosorg/850/2/big/21647695485012850_Cw.jpg",
            "//cdn2.focus.bg/mobile/photosorg/850/2/big/21647695485012850_1g.jpg",
            "//cdn2.focus.bg/mobile/photosorg/850/2/big/21647695485012850_S0.jpg",
            "//cdn2.focus.bg/mobile/photosorg/850/2/big/21647695485012850_xQ.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_007 = new()
    {
        RemoteId = "68156462",
        RemoteSlug = "toyota-prius-1-8-benzin-hibrit",
        Title = "Toyota Prius 1,8 бензин хибрит",
        Description = "Обслужена",
        PriceInBgn = 54000,
        BodyStyle = "хечбек",
        Engine = "хибриден",
        Transmission = "автоматична",
        MileageInKm = 44000,
        Condition = "употребяван",
        ManufacturedOn = new DateTime(2016, 8, 1),
        PowerInHp = 140,
        Color = "светло сив",
        PopulatedPlace = "Варна",
        Brand = "Toyota",
        Model = "Prius",
        Region = null,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic1.focus.bg/mobile/photosorg/077/1/big/11625296974013077_Yu.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/077/1/big/11625296974013077_fZ.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/077/1/big/11625296974013077_q4.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/077/1/big/11625296974013077_1U.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_008 = new()
    {
        RemoteId = "78524242",
        RemoteSlug = "mercedes-benz-e-350-cdi-avantgarde",
        Title = "Mercedes-Benz E 350 CDI Avantgarde",
        Description = "Произведен през месец декември 2009 г. , закупен от частно лице в Германия, реални километри, проверен в Силвър Стар - Варна. " +
        "Автомобилът разполага със следното оборудване: дизайн и ниво на оборудване Aвангард (Avantgarde), автоматична 7-степенна трансмисия с " +
        "възможност за смяна на предавките от волана, автоматични завиващи биксенонови предни светлини, кожен салон, кожен волан, темпомат със " +
        "спирачна система Спийдтроник (Speedtronic), електронни топлоизолационни стъкла, електрически сгъваеми странични огледала; фарове за мъгла," +
        " дневни лед светлини, централно заключване, система за разпознаване на умора на водача Атеншън Асист (Attention-Assist), асистент за " +
        "паркиране, аудио-навигационна система, отопляеми предни седалки; стъклопочистваща система с подгрев; aктивен преден капак, филтър за твърди " +
        "частици; бордкомпютър, система за следене на налягането в гумите; задни лед светлини, интегрирани мигачи в огледалата за странично виждане; " +
        "имобилайзер, подлавници Нек-про (Neck-pro), увеличен резервоар с лост на волана и възможност за ръчна смяна на предавките посредством " +
        "допълнитени бутони. Налични адаптивни, завиващи биксенонови предни светлини, автоматични лед дневни светлини, амбиентно регулируемо осветление" +
        " в купето, двузонов климатроник, падащи задни седалки, оригинални 17-цолови джанти, електрически седалки, сензори за дъжд и за светлина и " +
        "др. Автомобилът е в перфектно визуално и техническо състояние, без увреждания по боята и кожения салон, без ръжда, с оригинални филтри за " +
        "твърди частици и перфектна работа на агрегата. Напълно обслужен, сменено масло на трансмисията, поставени нови оригинални дискове и накладки," +
        " с нови зимни и летни гуми, ходова част без забележки. Автомобилът е разполага със сервизна история, без удари е, с два ключа, регистриран, " +
        "пълно автокаско и платени винетна такса и задължителна застраховка ГО.",
        PriceInBgn = 23999,
        BodyStyle = BodyStyleNames.Sedan,
        Engine = EngineNames.Diesel,
        Transmission = TransmissionNames.Automatic,
        MileageInKm = 260000,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2010, 1, 1),
        PowerInHp = 231,
        Color = ColorNames.LightGray,
        PopulatedPlace = "Шумен",
        Brand = BrandNames.MercedesBenz,
        Model = "E 350",
        Region = null,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_R4.png",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_ny.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_P6.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_R8.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_iA.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_PT.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_Wx.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_Si.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_5M.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/036/1/big/11606854971240036_qP.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_009 = new()
    {
        RemoteId = "82790797",
        RemoteSlug = "lexus-nx-350-awd-2-4l-nalichen",
        Title = "Lexus NX 350 AWD 2.4L НАЛИЧЕН",
        Description = "НОВ LEXUS NX 350 PREMIUM AWD, двигател 2. 4L с мощност 275 к. с. , 8-степенна автоматична скоростна кутия, размер гуми " +
        "P235/60HR18, задвижване 4х4. Оборудване: интериор кожен салон, избираеми режими за шофиране еко и спорт, навигационна система, камера за " +
        "задно виждане, темпомат, двузонов климатроник с гласово активиране, безключов достъп, Bluetooth, USB/12V изводи, подгряване и вентилация на" +
        " предни седалки, ел. регулиране на предни седалки с 8 степени, ел. регулиране на волана, преден и заден чашодържач, асистент за спускане по " +
        "наклон, асистент за задържане на наклон, ел. паркинг спирачка, асистент за мъртва точка, предупреждение за челен сблъсък и задно напречно " +
        "движение, система за контрол на налягането в гумите, сензор за дъжд, халогени, LED стоп светлини, автоматични дълги светлини, двоен шибидах. " +
        "Трейд Ин доставя за Вас всички марки и модели автомобили, произведени за американския пазар. Разполагаме със собствена сервизна база за поддръжка на " +
        "автомобилите, закупени от нас, лизинг и обратно изкупуване.",
        PriceInBgn = 0,
        BodyStyle = BodyStyleNames.Jeep,
        Engine = EngineNames.Gasoline,
        Transmission = TransmissionNames.Automatic,
        MileageInKm = 0,
        Condition = ConditionNames.New,
        ManufacturedOn = new DateTime(2022, 3, 1),
        PowerInHp = 275,
        Color = ColorNames.Blue,
        PopulatedPlace = "Кладница",
        Brand = BrandNames.Lexus,
        Model = "NX",
        Region = RegionNames.Pernik,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.Village,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_Z2.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_cG.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_ua.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_rO.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_v0.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_DI.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_JC.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_8M.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_rC.jpg",
            "//mobistatic1.focus.bg/mobile/photosorg/976/2/big/21647438561242976_vB.jpg",
        }
    };

    public static readonly NormalizedAdvert Test_010 = new()
    {
        RemoteId = "94058504",
        RemoteSlug = "renault-scenic-2-0-16v",
        Title = "Renault Scenic 2.0 16v",
        Description = "Нов внос -Кожен салон -Подгрев на предно стъкло -Подгрев на огледала -Климатроник -Подгрев на седалки -Хладилна джабка " +
        "-Тракшън контрол - 4 ел. Стъкла - 140 конски сили Без точица ръжда, има една забележка и тя се вижда на снимките ! Коментар на цената, " +
        "може и бартер !",
        PriceInBgn = 2600,
        BodyStyle = BodyStyleNames.Van,
        Engine = EngineNames.Gasoline,
        Transmission = TransmissionNames.Manual,
        MileageInKm = 246363,
        Condition = ConditionNames.Used,
        ManufacturedOn = new DateTime(2003, 12, 1),
        PowerInHp = 140,
        Color = ColorNames.Green,
        PopulatedPlace = "Девня",
        Brand = BrandNames.Renault,
        Model = "Scenic",
        Region = RegionNames.Varna,
        EuroStandard = null,
        PopulatedPlaceType = PopulatedPlaceType.City,
        Site = SiteNames.AutoBg,
        ImageUrls = new[]
        {
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_fc.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_Wu.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_Oa.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_3T.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_Lg.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_HV.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_PY.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_4y.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_Gn.jpg",
            "//mobistatic2.focus.bg/mobile/photosorg/282/1/big/11647188804139282_TL.jpg",
        }
    };
}
