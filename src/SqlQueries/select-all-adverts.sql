select
	RemoteId,
	Title,
	[Description],
	PriceBgn,
	ManufacturedOn,
	Kilometrage,
	HorsePowers,
	b.[Name] as Brand,
	m.[Name] as Model,
	tr.[Name] as Transmission,
	e.[Name] as Engine,
	col.[Name] as Color,
	con.[Name] as Condition,
	r.[Name] as Region,
	pp.[Name] as Town
from Adverts
left join Brands as b on b.Id = BrandId
left join Transmissions as tr on tr.Id = TransmissionId
left join Engines as e on e.Id = EngineId
left join Colors as col on col.Id = ColorId
left join Conditions as con on con.Id = ConditionId
left join Regions as r on r.Id = RegionId
left join PopulatedPlaces as pp on pp.Id = PopulatedPlaceId
left join Models as m on m.Id = ModelId