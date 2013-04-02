BEGIN TRANSACTION

-- TABLE agtAgents
ALTER TABLE agtAgents DROP CONSTRAINT agtAgents_PK
ALTER TABLE agtAgents ALTER COLUMN agentID BIGINT NOT NULL
ALTER TABLE agtAgents ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE agtAgents SET itemID = agentID

ALTER TABLE agtAgents ADD CONSTRAINT agtAgents_PK PRIMARY KEY (itemID)
ALTER TABLE agtAgents ADD CONSTRAINT agtAgents_IX_agentID UNIQUE(agentID)

DROP INDEX agtAgents_IX_corporation ON agtAgents
ALTER TABLE agtAgents ALTER COLUMN corporationID BIGINT NULL
CREATE INDEX agtAgents_IX_corporation ON agtAgents (corporationID)

DROP INDEX agtAgents_IX_station ON agtAgents
ALTER TABLE agtAgents ALTER COLUMN locationID BIGINT NULL
CREATE INDEX agtAgents_IX_station ON agtAgents (locationID)

-- TABLE chrBloodlines
ALTER TABLE chrBloodlines ALTER COLUMN corporationID BIGINT

-- TABLE chrFactions
ALTER TABLE chrFactions DROP CONSTRAINT chrFactions_PK
ALTER TABLE chrFactions ALTER COLUMN factionID BIGINT NOT NULL
ALTER TABLE chrFactions ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE chrFactions SET itemID = factionID

ALTER TABLE chrFactions ADD CONSTRAINT chrFactions_PK PRIMARY KEY (itemID)
ALTER TABLE chrFactions ADD CONSTRAINT chrFactions_IX_factionID UNIQUE(factionID)
ALTER TABLE chrFactions ALTER COLUMN corporationID BIGINT NULL
ALTER TABLE chrFactions ALTER COLUMN militiaCorporationID BIGINT NULL
ALTER TABLE chrFactions ALTER COLUMN solarSystemID BIGINT NULL

-- TABLE crpNPCCorporationDivisions
ALTER TABLE crpNPCCorporationDivisions DROP CONSTRAINT crpNPCCorporationDivisions_PK
ALTER TABLE crpNPCCorporationDivisions ALTER COLUMN corporationID BIGINT NOT NULL
ALTER TABLE crpNPCCorporationDivisions ADD CONSTRAINT crpNPCCorporationDivisions_PK PRIMARY KEY (corporationID, divisionID)

-- TABLE crpNPCCorporationResearchFields
ALTER TABLE crpNPCCorporationResearchFields DROP CONSTRAINT crpNPCCorporationResearchFields_PK
ALTER TABLE crpNPCCorporationResearchFields ALTER COLUMN corporationID BIGINT NOT NULL
ALTER TABLE crpNPCCorporationResearchFields ADD CONSTRAINT crpNPCCorporationResearchFields_PK PRIMARY KEY (corporationID, skillID)

-- TABLE crpNPCCorporationTrades
ALTER TABLE crpNPCCorporationTrades DROP CONSTRAINT crpNPCCorporationTrades_PK
ALTER TABLE crpNPCCorporationTrades ALTER COLUMN corporationID BIGINT NOT NULL
ALTER TABLE crpNPCCorporationTrades ADD CONSTRAINT crpNPCCorporationTrades_PK PRIMARY KEY (corporationID, typeID)

-- TABLE crpNPCCorporations
ALTER TABLE crpNPCCorporations DROP CONSTRAINT crpNPCCorporations_PK
ALTER TABLE crpNPCCorporations ALTER COLUMN corporationID BIGINT NOT NULL
ALTER TABLE crpNPCCorporations ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE crpNPCCorporations SET itemID = corporationID

ALTER TABLE crpNPCCorporations ADD CONSTRAINT crpNPCCorporations_PK PRIMARY KEY (itemID)
ALTER TABLE crpNPCCorporations ADD CONSTRAINT crpNPCCorporations_IX_corporationID UNIQUE(corporationID)

ALTER TABLE crpNPCCorporations ALTER COLUMN enemyID BIGINT NULL
ALTER TABLE crpNPCCorporations ALTER COLUMN friendID BIGINT NULL
ALTER TABLE crpNPCCorporations ALTER COLUMN investorID1 BIGINT NULL
ALTER TABLE crpNPCCorporations ALTER COLUMN investorID2 BIGINT NULL
ALTER TABLE crpNPCCorporations ALTER COLUMN investorID3 BIGINT NULL
ALTER TABLE crpNPCCorporations ALTER COLUMN investorID4 BIGINT NULL
ALTER TABLE crpNPCCorporations ALTER COLUMN solarSystemID BIGINT NULL

-- TABLE invBlueprintTypes
ALTER TABLE invBlueprintTypes DROP CONSTRAINT invBlueprintTypes_PK
ALTER TABLE invBlueprintTypes ADD typeID INT DEFAULT 0 NOT NULL
GO
UPDATE invBlueprintTypes SET typeID = blueprintTypeID

ALTER TABLE invBlueprintTypes ADD CONSTRAINT invBlueprintTypes_PK PRIMARY KEY (typeID)
ALTER TABLE invBlueprintTypes ADD CONSTRAINT invBlueprintTypes_IX_blueprintTypeID UNIQUE(blueprintTypeID)

-- TABLE invItems
DROP INDEX items_IX_OwnerLocation ON invItems
ALTER TABLE invItems ALTER COLUMN ownerID BIGINT NULL
CREATE INDEX items_IX_OwnerLocation ON invItems (ownerID, locationID)

-- TABLE invUniqueNames
ALTER TABLE invUniqueNames DROP CONSTRAINT invUniqueNames_PK
ALTER TABLE invUniqueNames ALTER COLUMN itemID BIGINT NOT NULL
ALTER TABLE invUniqueNames ADD CONSTRAINT invUniqueNames_PK PRIMARY KEY (itemID)

-- TABLE mapCelestialStatistics
ALTER TABLE mapCelestialStatistics DROP CONSTRAINT mapCelestialStatistics_PK
ALTER TABLE mapCelestialStatistics ALTER COLUMN celestialID BIGINT NOT NULL
ALTER TABLE mapCelestialStatistics ADD CONSTRAINT mapCelestialStatistics_PK PRIMARY KEY (celestialID)

-- TABLE mapConstellationJumps
ALTER TABLE mapConstellationJumps DROP CONSTRAINT mapConstellationJumps_PK
DROP INDEX mapConstellationJumps_IX_fromRegion ON mapConstellationJumps
ALTER TABLE mapConstellationJumps ALTER COLUMN fromConstellationID BIGINT NOT NULL
ALTER TABLE mapConstellationJumps ALTER COLUMN fromRegionID BIGINT NOT NULL
ALTER TABLE mapConstellationJumps ALTER COLUMN toConstellationID BIGINT NOT NULL
ALTER TABLE mapConstellationJumps ALTER COLUMN toRegionID BIGINT NOT NULL
ALTER TABLE mapConstellationJumps ADD CONSTRAINT mapConstellationJumps_PK PRIMARY KEY (fromConstellationID, toConstellationID)
CREATE INDEX mapConstellationJumps_IX_fromRegion ON mapConstellationJumps (fromRegionID)

-- TABLE mapConstellations
ALTER TABLE mapConstellations DROP CONSTRAINT mapConstellations_PK
ALTER TABLE mapConstellations ALTER COLUMN constellationID BIGINT NOT NULL
ALTER TABLE mapConstellations ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE mapConstellations SET itemID = constellationID

ALTER TABLE mapConstellations ADD CONSTRAINT mapConstellations_PK PRIMARY KEY (itemID)
ALTER TABLE mapConstellations ADD CONSTRAINT mapConstellations_IX_constellationID UNIQUE(constellationID)

DROP INDEX mapConstellations_IX_region ON mapConstellations
ALTER TABLE mapConstellations ALTER COLUMN regionID BIGINT NULL
CREATE INDEX mapConstellations_IX_region ON mapConstellations (regionID)

-- TABLE mapDenormalize
ALTER TABLE mapDenormalize DROP CONSTRAINT mapDenormalize_PK
ALTER TABLE mapDenormalize ALTER COLUMN itemID BIGINT NOT NULL
ALTER TABLE mapDenormalize ADD CONSTRAINT mapDenormalize_PK PRIMARY KEY (itemID)

DROP INDEX mapDenormalize_IX_constellation ON mapDenormalize
DROP INDEX mapDenormalize_IX_groupConstellation ON mapDenormalize
DROP INDEX mapDenormalize_IX_groupRegion ON mapDenormalize
DROP INDEX mapDenormalize_IX_groupSystem ON mapDenormalize
DROP INDEX mapDenormalize_IX_orbit ON mapDenormalize
DROP INDEX mapDenormalize_IX_region ON mapDenormalize
DROP INDEX mapDenormalize_IX_system ON mapDenormalize

ALTER TABLE mapDenormalize ALTER COLUMN constellationID BIGINT NULL
ALTER TABLE mapDenormalize ALTER COLUMN orbitID BIGINT NULL
ALTER TABLE mapDenormalize ALTER COLUMN regionID BIGINT NULL
ALTER TABLE mapDenormalize ALTER COLUMN solarSystemID BIGINT NULL

CREATE INDEX mapDenormalize_IX_constellation ON mapDenormalize (constellationID)
CREATE INDEX mapDenormalize_IX_groupConstellation ON mapDenormalize (groupID, constellationID)
CREATE INDEX mapDenormalize_IX_groupRegion ON mapDenormalize (groupID, regionID)
CREATE INDEX mapDenormalize_IX_groupSystem ON mapDenormalize (groupID, solarSystemID)
CREATE INDEX mapDenormalize_IX_orbit ON mapDenormalize (orbitID)
CREATE INDEX mapDenormalize_IX_region ON mapDenormalize (regionID)
CREATE INDEX mapDenormalize_IX_system ON mapDenormalize (solarSystemID)

-- TABLE mapJumps
ALTER TABLE mapJumps DROP CONSTRAINT mapJumps_PK
ALTER TABLE mapJumps ALTER COLUMN stargateID BIGINT NOT NULL
ALTER TABLE mapJumps ADD CONSTRAINT mapJumps_PK PRIMARY KEY (stargateID)

ALTER TABLE mapJumps ALTER COLUMN celestialID BIGINT NULL

-- TABLE mapLandmarks
ALTER TABLE mapLandmarks ALTER COLUMN locationID BIGINT NULL

-- TABLE mapLocationScenes
ALTER TABLE mapLocationScenes DROP CONSTRAINT mapLocationScenes_PK_ -- sic
ALTER TABLE mapLocationScenes ALTER COLUMN locationID BIGINT NOT NULL
ALTER TABLE mapLocationScenes ADD CONSTRAINT mapLocationScenes_PK_ PRIMARY KEY (locationID)

-- TABLE mapLocationWormholeClasses
ALTER TABLE mapLocationWormholeClasses DROP CONSTRAINT mapLocationWormholeClasses_PK -- sic
ALTER TABLE mapLocationWormholeClasses ALTER COLUMN locationID BIGINT NOT NULL
ALTER TABLE mapLocationWormholeClasses ADD CONSTRAINT mapLocationWormholeClasses_PK PRIMARY KEY (locationID)

-- TABLE mapRegionJumps
ALTER TABLE mapRegionJumps DROP CONSTRAINT mapRegionJumps_PK
ALTER TABLE mapRegionJumps ALTER COLUMN fromRegionID BIGINT NOT NULL
ALTER TABLE mapRegionJumps ALTER COLUMN toRegionID BIGINT NOT NULL
ALTER TABLE mapRegionJumps ADD CONSTRAINT mapRegionJumps_PK PRIMARY KEY (fromRegionID, toRegionID)

-- TABLE mapRegions
ALTER TABLE mapRegions DROP CONSTRAINT mapRegions_PK
ALTER TABLE mapRegions ALTER COLUMN regionID BIGINT NOT NULL
ALTER TABLE mapRegions ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE mapRegions SET itemID = regionID
ALTER TABLE mapRegions ADD CONSTRAINT mapRegions_PK PRIMARY KEY (itemID)
ALTER TABLE mapRegions ADD CONSTRAINT mapRegions_IX_regionID UNIQUE(regionID)

ALTER TABLE mapRegions ALTER COLUMN factionID BIGINT NULL

-- TABLE mapSolarSystemJumps
ALTER TABLE mapSolarSystemJumps DROP CONSTRAINT mapSolarSystemJumps_PK
DROP INDEX mapSolarSystemJumps_IX_fromConstellation ON mapSolarSystemJumps
DROP INDEX mapSolarSystemJumps_IX_fromRegion ON mapSolarSystemJumps
ALTER TABLE mapSolarSystemJumps ALTER COLUMN fromConstellationID BIGINT NOT NULL
ALTER TABLE mapSolarSystemJumps ALTER COLUMN fromRegionID BIGINT NOT NULL
ALTER TABLE mapSolarSystemJumps ALTER COLUMN fromSolarSystemID BIGINT NOT NULL
ALTER TABLE mapSolarSystemJumps ALTER COLUMN toConstellationID BIGINT NOT NULL
ALTER TABLE mapSolarSystemJumps ALTER COLUMN toRegionID BIGINT NOT NULL
ALTER TABLE mapSolarSystemJumps ALTER COLUMN toSolarSystemID BIGINT NOT NULL
ALTER TABLE mapSolarSystemJumps ADD CONSTRAINT mapSolarSystemJumps_PK PRIMARY KEY (fromSolarSystemID, toSolarSystemID)
CREATE INDEX mapSolarSystemJumps_IX_fromConstellation ON mapSolarSystemJumps (fromConstellationID)
CREATE INDEX mapSolarSystemJumps_IX_fromRegion ON mapSolarSystemJumps (fromRegionID)

-- TABLE mapSolarSystems
ALTER TABLE mapSolarSystems DROP CONSTRAINT mapSolarSystems_PK
ALTER TABLE mapSolarSystems ALTER COLUMN solarSystemID BIGINT NOT NULL
ALTER TABLE mapSolarSystems ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE mapSolarSystems SET itemID = solarSystemID

ALTER TABLE mapSolarSystems ADD CONSTRAINT mapSolarSystems_PK PRIMARY KEY (itemID)
ALTER TABLE mapSolarSystems ADD CONSTRAINT mapSolarSystems_IX_solarSystemID UNIQUE(solarSystemID)

DROP INDEX mapSolarSystems_IX_constellation ON mapSolarSystems
DROP INDEX mapSolarSystems_IX_region ON mapSolarSystems
ALTER TABLE mapSolarSystems ALTER COLUMN constellationID BIGINT NULL
ALTER TABLE mapSolarSystems ALTER COLUMN factionID BIGINT NULL
ALTER TABLE mapSolarSystems ALTER COLUMN regionID BIGINT NULL
CREATE INDEX mapSolarSystems_IX_constellation ON mapSolarSystems (constellationID)
CREATE INDEX mapSolarSystems_IX_region ON mapSolarSystems (regionID)

-- TABLE mapUniverse
ALTER TABLE mapUniverse DROP CONSTRAINT mapUniverse_PK
ALTER TABLE mapUniverse ALTER COLUMN universeID BIGINT NOT NULL
ALTER TABLE mapUniverse ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE mapUniverse SET itemID = universeID

ALTER TABLE mapUniverse ADD CONSTRAINT mapUniverse_PK PRIMARY KEY (itemID)
ALTER TABLE mapUniverse ADD CONSTRAINT mapUniverse_IX_universeID UNIQUE(universeID)

-- TABLE ramAssemblyLineStations
ALTER TABLE ramAssemblyLineStations DROP CONSTRAINT ramAssemblyLineStations_PK
ALTER TABLE ramAssemblyLineStations ALTER COLUMN stationID BIGINT NOT NULL
ALTER TABLE ramAssemblyLineStations ADD CONSTRAINT ramAssemblyLineStations_PK PRIMARY KEY (stationID, assemblyLineTypeID)

DROP INDEX ramAssemblyLineStations_IX_owner ON ramAssemblyLineStations
DROP INDEX ramAssemblyLineStations_IX_region ON ramAssemblyLineStations
ALTER TABLE ramAssemblyLineStations ALTER COLUMN ownerID BIGINT NULL
ALTER TABLE ramAssemblyLineStations ALTER COLUMN solarSystemID BIGINT NULL
ALTER TABLE ramAssemblyLineStations ALTER COLUMN regionID BIGINT NULL
CREATE INDEX ramAssemblyLineStations_IX_owner ON ramAssemblyLineStations (ownerID)
CREATE INDEX ramAssemblyLineStations_IX_region ON ramAssemblyLineStations (regionID)

-- TABLE ramAssemblyLineStations
ALTER TABLE ramAssemblyLineStations DROP CONSTRAINT ramAssemblyLineStations_PK
ALTER TABLE ramAssemblyLineStations ALTER COLUMN stationID BIGINT NOT NULL
ALTER TABLE ramAssemblyLineStations ADD CONSTRAINT ramAssemblyLineStations_PK PRIMARY KEY (stationID, assemblyLineTypeID)

DROP INDEX ramAssemblyLineStations_IX_owner ON ramAssemblyLineStations
DROP INDEX ramAssemblyLineStations_IX_region ON ramAssemblyLineStations
ALTER TABLE ramAssemblyLineStations ALTER COLUMN ownerID BIGINT NULL
ALTER TABLE ramAssemblyLineStations ALTER COLUMN solarSystemID BIGINT NULL
ALTER TABLE ramAssemblyLineStations ALTER COLUMN regionID BIGINT NULL
CREATE INDEX ramAssemblyLineStations_IX_owner ON ramAssemblyLineStations (ownerID)
CREATE INDEX ramAssemblyLineStations_IX_region ON ramAssemblyLineStations (regionID)

-- TABLE ramAssemblyLines
DROP INDEX ramAssemblyLines_IX_owner ON ramAssemblyLines
DROP INDEX ramAssemblyLines_IX_container ON ramAssemblyLines
ALTER TABLE ramAssemblyLines ALTER COLUMN ownerID BIGINT NULL
ALTER TABLE ramAssemblyLines ALTER COLUMN containerID BIGINT NULL
CREATE INDEX ramAssemblyLines_IX_owner ON ramAssemblyLines (ownerID)
CREATE INDEX ramAssemblyLines_IX_container ON ramAssemblyLines (containerID)

-- TABLE staStations
DROP INDEX ramAssemblyLines_IX_owner ON ramAssemblyLines
DROP INDEX ramAssemblyLines_IX_container ON ramAssemblyLines
ALTER TABLE ramAssemblyLines ALTER COLUMN ownerID BIGINT NULL
ALTER TABLE ramAssemblyLines ALTER COLUMN containerID BIGINT NULL
CREATE INDEX ramAssemblyLines_IX_owner ON ramAssemblyLines (ownerID)
CREATE INDEX ramAssemblyLines_IX_container ON ramAssemblyLines (containerID)

-- TABLE staStations
ALTER TABLE staStations DROP CONSTRAINT staStations_PK
ALTER TABLE staStations ALTER COLUMN stationID BIGINT NOT NULL
ALTER TABLE staStations ADD itemID BIGINT DEFAULT 0 NOT NULL
GO
UPDATE staStations SET itemID = stationID

ALTER TABLE staStations ADD CONSTRAINT staStations_PK PRIMARY KEY (itemID)
ALTER TABLE staStations ADD CONSTRAINT staStations_IX_stationID UNIQUE(stationID)

DROP INDEX staStations_IX_constellation ON staStations
DROP INDEX staStations_IX_corporation ON staStations
DROP INDEX staStations_IX_region ON staStations
DROP INDEX staStations_IX_system ON staStations
ALTER TABLE staStations ALTER COLUMN constellationID BIGINT NULL
ALTER TABLE staStations ALTER COLUMN corporationID BIGINT NULL
ALTER TABLE staStations ALTER COLUMN regionID BIGINT NULL
ALTER TABLE staStations ALTER COLUMN solarSystemID BIGINT NULL
CREATE INDEX staStations_IX_constellation ON staStations (constellationID)
CREATE INDEX staStations_IX_corporation ON staStations (corporationID)
CREATE INDEX staStations_IX_region ON staStations (regionID)
CREATE INDEX staStations_IX_system ON staStations (solarSystemID)

-- TABLE warCombatZoneSystems
ALTER TABLE warCombatZoneSystems DROP CONSTRAINT combatZoneSystems_PK -- sic
ALTER TABLE warCombatZoneSystems ALTER COLUMN solarSystemID BIGINT NOT NULL
ALTER TABLE warCombatZoneSystems ADD CONSTRAINT combatZoneSystems_PK PRIMARY KEY (solarSystemID)


COMMIT TRANSACTION