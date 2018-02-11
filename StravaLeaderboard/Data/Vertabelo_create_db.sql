-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-02-11 18:26:14.85

-- tables
-- Table: Activity
CREATE TABLE Activity (
    ActivityId integer NOT NULL CONSTRAINT Activity_pk PRIMARY KEY,
    Name text NOT NULL,
    Start_Date datetime NOT NULL,
    Achievement_count integer NOT NULL,
    Comment_count integer NOT NULL,
    Kudos_count integer NOT NULL,
    Flagged boolean NOT NULL,
    Event_EventId integer NOT NULL,
    Athlete_AthleteId integer NOT NULL,
    CONSTRAINT Activity_Event FOREIGN KEY (Event_EventId)
    REFERENCES Event (EventId),
    CONSTRAINT Activity_Athlete FOREIGN KEY (Athlete_AthleteId)
    REFERENCES Athlete (AthleteId)
);

-- Table: ActivityResults
CREATE TABLE ActivityResults (
    ActivityResultsId integer NOT NULL CONSTRAINT ActivityResults_pk PRIMARY KEY,
    Rank integer NOT NULL,
    Elapsed_time integer NOT NULL,
    Start_date datetime NOT NULL,
    Points integer NOT NULL,
    Personal_best boolean NOT NULL DEFAULT False,
    Activity_ActivityId integer NOT NULL,
    Segment_SegmentId integer NOT NULL,
    CONSTRAINT ActivityResults_Activity FOREIGN KEY (Activity_ActivityId)
    REFERENCES Activity (ActivityId),
    CONSTRAINT ActivityResults_Segment FOREIGN KEY (Segment_SegmentId)
    REFERENCES Segment (SegmentId)
);

-- Table: Athlete
CREATE TABLE Athlete (
    AthleteId integer NOT NULL CONSTRAINT Athlete_pk PRIMARY KEY,
    UserName text NOT NULL,
    Sex text NOT NULL,
    FirstName text NOT NULL,
    LastName text NOT NULL,
    Profile text NOT NULL,
    Access_token text NOT NULL,
    Date_joined date NOT NULL
);

-- Table: AthleteEventResults
CREATE TABLE AthleteEventResults (
    AthleteEventResultsId integer NOT NULL CONSTRAINT AthleteEventResults_pk PRIMARY KEY,
    Green_points integer NOT NULL,
    Polka_points integer NOT NULL,
    Activity_ActivityId integer NOT NULL,
    CONSTRAINT EventResults_Activity FOREIGN KEY (Activity_ActivityId)
    REFERENCES Activity (ActivityId)
);

-- Table: Club
CREATE TABLE Club (
    ClubId integer NOT NULL CONSTRAINT Club_pk PRIMARY KEY,
    Name text NOT NULL,
    Location text NOT NULL,
    Country text NOT NULL
);

-- Table: Event
CREATE TABLE Event (
    EventId integer NOT NULL CONSTRAINT Event_pk PRIMARY KEY,
    Date date NOT NULL,
    Season_SeasonId integer NOT NULL,
    CONSTRAINT Event_Season FOREIGN KEY (Season_SeasonId)
    REFERENCES Season (SeasonId)
);

-- Table: EventSegment
CREATE TABLE EventSegment (
    EventSegmentId integer NOT NULL CONSTRAINT EventSegment_pk PRIMARY KEY,
    Event_EventId integer NOT NULL,
    Segment_SegmentId integer NOT NULL,
    CONSTRAINT EventSegment_Event FOREIGN KEY (Event_EventId)
    REFERENCES Event (EventId),
    CONSTRAINT EventSegment_Segment FOREIGN KEY (Segment_SegmentId)
    REFERENCES Segment (SegmentId)
);

-- Table: JSONActivity
CREATE TABLE JSONActivity (
    ActivityId integer NOT NULL CONSTRAINT JSONActivity_pk PRIMARY KEY,
    Name text NOT NULL,
    Start_Date datetime NOT NULL,
    Achievement_count integer NOT NULL,
    Comment_count integer NOT NULL,
    Kudos_count integer NOT NULL,
    Flagged boolean NOT NULL,
    AthleteId integer NOT NULL,
    Athlete_Id integer NOT NULL,
    CONSTRAINT JActivity_JAthlete FOREIGN KEY (Athlete_Id)
    REFERENCES JSONAthlete (AthleteId)
);

-- Table: JSONAthlete
CREATE TABLE JSONAthlete (
    AthleteId integer NOT NULL CONSTRAINT JSONAthlete_pk PRIMARY KEY,
    UserName text NOT NULL,
    Sex text NOT NULL,
    FirstName text NOT NULL,
    LastName text NOT NULL,
    Profile text NOT NULL
);

-- Table: JSONResults
CREATE TABLE JSONResults (
    SegmentId integer NOT NULL CONSTRAINT JSONResults_pk PRIMARY KEY,
    Type text NOT NULL,
    Rank integer NOT NULL,
    ElapsedTime datetime NOT NULL,
    Athlete_AthleteId integer NOT NULL,
    CONSTRAINT SegmentResults_Athlete FOREIGN KEY (Athlete_AthleteId)
    REFERENCES JSONAthlete (AthleteId)
);

-- Table: RAWEntries
CREATE TABLE RAWEntries (
    EntriesId integer NOT NULL CONSTRAINT RAWEntries_pk PRIMARY KEY,
    Athlete_name text NOT NULL,
    Elapsed_time datetime NOT NULL,
    Rank integer NOT NULL,
    Start_date datetime NOT NULL,
    RAWResults_SegmentId integer NOT NULL,
    CONSTRAINT RAWEntries_RAWResults FOREIGN KEY (RAWResults_SegmentId)
    REFERENCES RAWResults (SegmentId)
);

-- Table: RAWResults
CREATE TABLE RAWResults (
    SegmentId integer NOT NULL CONSTRAINT RAWResults_pk PRIMARY KEY,
    Entry_count integer NOT NULL
);

-- Table: Season
CREATE TABLE Season (
    SeasonId integer NOT NULL CONSTRAINT Season_pk PRIMARY KEY,
    Start_date date NOT NULL,
    End_date date NOT NULL,
    Club_ClubId integer NOT NULL,
    CONSTRAINT Season_Club FOREIGN KEY (Club_ClubId)
    REFERENCES Club (ClubId)
);

-- Table: SeasonAthlete
CREATE TABLE SeasonAthlete (
    SeasonAthleteId integer NOT NULL CONSTRAINT SeasonAthlete_pk PRIMARY KEY,
    Season_SeasonId integer NOT NULL,
    Athlete_AthleteId integer NOT NULL,
    CONSTRAINT SeasonAthlete_Season FOREIGN KEY (Season_SeasonId)
    REFERENCES Season (SeasonId),
    CONSTRAINT SeasonAthlete_Athlete FOREIGN KEY (Athlete_AthleteId)
    REFERENCES Athlete (AthleteId)
);

-- Table: SeasonLeaderboard
CREATE TABLE SeasonLeaderboard (
    SeasonLeaderboardId integer NOT NULL CONSTRAINT SeasonLeaderboard_pk PRIMARY KEY,
    Total_points integer NOT NULL,
    Total_adj_points integer NOT NULL,
    Total_green_points integer NOT NULL,
    Total_polka_points integer NOT NULL,
    Date_started date NOT NULL,
    Events_attended integer NOT NULL,
    Athlete_AthleteId integer NOT NULL,
    Season_SeasonId integer NOT NULL,
    CONSTRAINT SeasonLeaderboard_Athlete FOREIGN KEY (Athlete_AthleteId)
    REFERENCES Athlete (AthleteId),
    CONSTRAINT SeasonLeaderboard_Season FOREIGN KEY (Season_SeasonId)
    REFERENCES Season (SeasonId)
);

-- Table: Segment
CREATE TABLE Segment (
    SegmentId integer NOT NULL CONSTRAINT Segment_pk PRIMARY KEY,
    Type text NOT NULL,
    Name text NOT NULL,
    Distance integer NOT NULL,
    Average_grade integer NOT NULL,
    Total_elevation_gain integer NOT NULL,
    Star_count integer NOT NULL
);

-- End of file.

