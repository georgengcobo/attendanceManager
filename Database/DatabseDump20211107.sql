CREATE DATABASE  IF NOT EXISTS `attendance_register` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `attendance_register`;
-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: localhost    Database: attendance_register
-- ------------------------------------------------------
-- Server version	5.7.30-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `attendance`
--

DROP TABLE IF EXISTS `attendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attendance` (
  `AttendanceId` int(11) NOT NULL AUTO_INCREMENT,
  `AttendanceDatetime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `RegistrationId` int(11) NOT NULL,
  `TeacherId` int(11) NOT NULL,
  `IsPresent` tinyint(4) NOT NULL,
  PRIMARY KEY (`AttendanceId`),
  KEY `fk_valid_RegistrationId` (`RegistrationId`),
  KEY `fk_valid_TeacherId` (`TeacherId`),
  CONSTRAINT `fk_valid_RegistrationId` FOREIGN KEY (`RegistrationId`) REFERENCES `registration` (`RegistrationId`),
  CONSTRAINT `fk_valid_TeacherId` FOREIGN KEY (`TeacherId`) REFERENCES `teachers` (`TeacherId`),
  CONSTRAINT `fk_valid_teachers` FOREIGN KEY (`TeacherId`) REFERENCES `teachers` (`TeacherId`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendance`
--

LOCK TABLES `attendance` WRITE;
/*!40000 ALTER TABLE `attendance` DISABLE KEYS */;
INSERT INTO `attendance` VALUES (1,'2021-11-04 17:30:05',1,1,1),(2,'2021-11-04 17:30:24',1,1,1),(3,'2021-11-04 17:30:52',1,1,1),(4,'2021-11-04 17:36:19',1,1,1),(5,'2021-11-04 17:45:17',1,1,1),(6,'2021-11-04 17:45:26',1,1,1),(7,'2021-11-04 17:45:34',1,1,1),(8,'2021-11-04 17:51:53',1,1,1),(9,'2021-11-04 17:55:12',1,1,1),(10,'2021-11-05 15:27:38',1,1,1),(11,'2021-11-05 15:27:38',1,1,1),(12,'2021-11-05 15:27:38',1,1,1),(13,'2021-11-06 15:27:38',1,1,1),(14,'2021-11-06 21:21:00',6,1,1),(15,'2021-11-27 21:26:00',5,1,1),(16,'2021-09-30 13:56:00',6,1,1),(17,'2021-11-01 21:57:00',1,1,1),(18,'2021-11-01 21:57:00',3,1,1),(19,'2021-11-01 21:57:00',2,1,1),(20,'2021-11-08 20:44:00',6,1,1),(21,'2021-11-08 20:44:00',9,1,1),(22,'2021-11-13 20:44:00',9,1,0),(23,'2021-11-13 20:44:00',6,1,0),(24,'2021-11-07 21:01:00',7,1,0),(25,'2021-11-07 21:01:00',10,1,0),(26,'2021-11-07 21:01:00',4,1,0),(27,'2021-11-08 21:05:00',2,1,0),(28,'2021-11-08 21:05:00',1,1,0),(29,'2021-11-08 21:05:00',3,1,0);
/*!40000 ALTER TABLE `attendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `classes`
--

DROP TABLE IF EXISTS `classes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `classes` (
  `ClassId` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ClassName` varchar(45) NOT NULL,
  `Grade` int(11) NOT NULL,
  `TeacherId` int(11) NOT NULL,
  PRIMARY KEY (`ClassId`),
  UNIQUE KEY `classname_UNIQUE` (`ClassName`,`Grade`),
  KEY `fk_valid_TeachersId` (`TeacherId`),
  CONSTRAINT `fk_valid_TeachersId` FOREIGN KEY (`TeacherId`) REFERENCES `teachers` (`TeacherId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `classes`
--

LOCK TABLES `classes` WRITE;
/*!40000 ALTER TABLE `classes` DISABLE KEYS */;
INSERT INTO `classes` VALUES (3,'2021-11-04 14:30:44','Maths101',1,1),(5,'2021-11-04 14:38:13','Maths101',2,1),(6,'2021-11-04 14:38:29','Maths101',3,1),(7,'2021-11-04 14:38:38','English101',3,1),(8,'2021-11-07 14:19:45','Physics 202',10,2),(9,'2021-11-07 14:53:40','Music 202',12,4),(10,'2021-11-07 14:56:29','Technology 202',12,3),(11,'2021-11-07 15:01:19','Geometry',12,3),(12,'2021-11-07 21:05:39','Science 101',10,4);
/*!40000 ALTER TABLE `classes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registration`
--

DROP TABLE IF EXISTS `registration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `registration` (
  `RegistrationId` int(11) NOT NULL AUTO_INCREMENT,
  `RegistrationDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `ClassId` int(11) NOT NULL,
  `StudentId` int(11) NOT NULL,
  PRIMARY KEY (`RegistrationId`),
  KEY `fk_valid_student` (`StudentId`),
  KEY `fk_valid_classes` (`ClassId`),
  CONSTRAINT `fk_valid_classes` FOREIGN KEY (`ClassId`) REFERENCES `classes` (`ClassId`),
  CONSTRAINT `fk_valid_student` FOREIGN KEY (`StudentId`) REFERENCES `students` (`StudentId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registration`
--

LOCK TABLES `registration` WRITE;
/*!40000 ALTER TABLE `registration` DISABLE KEYS */;
INSERT INTO `registration` VALUES (1,'2021-11-04 16:06:50',5,1),(2,'2021-11-04 16:08:07',5,2),(3,'2021-11-04 16:08:11',5,3),(4,'2021-11-05 13:57:09',3,3),(5,'2021-11-05 13:57:20',6,3),(6,'2021-11-05 13:57:30',7,3),(7,'2021-11-07 08:45:07',3,4),(8,'2021-11-07 08:45:42',6,2),(9,'2021-11-07 08:45:54',7,2),(10,'2021-11-07 08:46:53',3,2);
/*!40000 ALTER TABLE `registration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `students` (
  `StudentId` int(11) NOT NULL AUTO_INCREMENT,
  `DateTimeAdded` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `IdNumber` varchar(45) NOT NULL,
  PRIMARY KEY (`StudentId`),
  UNIQUE KEY `IdNumber_UNIQUE` (`IdNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students`
--

LOCK TABLES `students` WRITE;
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
INSERT INTO `students` VALUES (1,'2021-11-04 16:00:28','Simon','Thring','1234568730'),(2,'2021-11-04 16:00:46','Brian','Adams','8308025817080'),(3,'2021-11-04 16:01:10','George','Dannies','8308025817055'),(4,'2021-11-07 08:13:11','James','Blunts','12355548932157'),(5,'2021-11-07 14:21:56','James','Ngubane','0235589736694'),(6,'2021-11-07 15:05:58','Bill','HillMan','8380251554866'),(7,'2021-11-07 15:06:17','Danny','Majero','8896654722549'),(8,'2021-11-07 15:17:15','Glen','Lewis','12345687910312'),(9,'2021-11-07 15:20:18','Crazy','Toothless','1234567890011'),(10,'2021-11-07 15:20:46','Major','Williams','1234567890123');
/*!40000 ALTER TABLE `students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teachers`
--

DROP TABLE IF EXISTS `teachers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teachers` (
  `TeacherId` int(10) NOT NULL AUTO_INCREMENT,
  `RegisteredTime` datetime DEFAULT CURRENT_TIMESTAMP,
  `Name` varchar(45) DEFAULT NULL,
  `Surname` varchar(15) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Email` varchar(65) NOT NULL,
  PRIMARY KEY (`TeacherId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teachers`
--

LOCK TABLES `teachers` WRITE;
/*!40000 ALTER TABLE `teachers` DISABLE KEYS */;
INSERT INTO `teachers` VALUES (1,'2021-11-03 20:06:32','George','Ngcobo','e10adc3949ba59abbe56e057f20f883e','george.s.ngcobo@gmail.com'),(2,'2021-11-04 10:32:29','Simone','Daniels','e10adc3949ba59abbe56e057f20f883e','Simone@gmail.com'),(3,'2021-11-06 17:40:29','Brian','Adams','0cc175b9c0f1b6a831c399e269772661','brian@gmail.com'),(4,'2021-11-06 17:50:55','Major','League','0cc175b9c0f1b6a831c399e269772661','major@gmail.com');
/*!40000 ALTER TABLE `teachers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'attendance_register'
--

--
-- Dumping routines for database 'attendance_register'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-07 21:11:27
