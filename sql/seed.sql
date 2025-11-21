-- SQL to create tables and seed sample data for sqlite
CREATE TABLE IF NOT EXISTS mst_subject (
  SubjectKey INTEGER PRIMARY KEY AUTOINCREMENT,
  SubjectName TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS mst_student (
  StudentKey INTEGER PRIMARY KEY AUTOINCREMENT,
  StudentName TEXT NOT NULL,
  SubjectKey INTEGER,
  Grade INTEGER,
  FOREIGN KEY(SubjectKey) REFERENCES mst_subject(SubjectKey)
);

INSERT INTO mst_subject (SubjectName) VALUES ('Mathematics'), ('English'), ('Science');

INSERT INTO mst_student (StudentName, SubjectKey, Grade) VALUES
('Ali', 1, 80),
('Fatima', 2, 72),
('Omar', 3, 90);
