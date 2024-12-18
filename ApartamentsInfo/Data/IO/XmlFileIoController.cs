using ApartamentsInfo.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ApartamentsInfo.Data.IO
{
    public class XmlFileIoController : IFileIoController
    {
        public string FileExtension
        {
            get { return ".xml"; }
        }

        public string FileTypeCaption
        {
            get { return "Файли формату XML"; }
        }

        

        //------------------------------WRITING--------------------------------------WRITING----------------------------------------------------WRITING------------------------------------------------------------
        void WriteOwners(IEnumerable<Owner> collection, XmlWriter writer)
        {
            writer.WriteStartElement("OwnersData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("Owner");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.name);
                int parentId = obj.Parent == null ? 0 : obj.Parent.Id;
                writer.WriteElementString("ParentId", parentId.ToString());
                writer.WriteElementString("PhoneNumber",obj.PhoneNumber.ToString());
                writer.WriteElementString("LegalEnity", obj.LegalEnity.ToString());
                writer.WriteElementString("Note", obj.Note);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        void WriteApartaments(IEnumerable<Apartament> collection, XmlWriter writer)
        {
            writer.WriteStartElement("ApartamentsData");
            foreach(var inst in collection)
            {
                writer.WriteStartElement("Apartament");
                writer.WriteElementString("Id", inst.Id.ToString());
                writer.WriteElementString("HouseNum", inst.houseNum);
                writer.WriteElementString("HouseFloor", inst.houseFloor.ToString());
                writer.WriteElementString("ApartNum", inst.apartNum.ToString());
                writer.WriteElementString("NumOfRooms", inst.numOfRooms.ToString());
                writer.WriteElementString("Description", inst.Description);
                int ownerId = inst.Owner == null ? 0 : inst.Owner.Id;
                writer.WriteElementString("Owner", ownerId.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        void WriteData(IDataSet dataSet, XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("ApartamentsInfo");
            WriteOwners(dataSet.Owners, writer);
            WriteApartaments(dataSet.Apartaments, writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();

        }
        //------------------------------READING--------------------------------------READING----------------------------------------------------READING------------------------------------------------------------
        void ReadOwner(XmlReader reader, ICollection<Owner> collection)
        {
            Owner obj = new Owner();
            reader.ReadStartElement("Owner");
            obj.Id = reader.ReadElementContentAsInt();
            obj.name = reader.ReadElementContentAsString();
            int parentId = reader.ReadElementContentAsInt();
            Owner Parent = collection.FirstOrDefault(e => e.Id == parentId);
            obj.PhoneNumber = reader.ReadElementContentAsString();
            string legalEnity = reader.ReadElementContentAsString();
            if(legalEnity == "True")
            {
                obj.LegalEnity = true;
            }
            else
            {
                obj.LegalEnity = false;
            }
            obj.Note = reader.ReadElementContentAsString();
            collection.Add(obj);
        }

        void ReadApartament(XmlReader reader, IDataSet dataSet)
        {
            Apartament inst = new Apartament();
            reader.ReadStartElement("Apartament");
            inst.Id = reader.ReadElementContentAsInt();
            inst.houseNum = reader.ReadElementContentAsString();
            inst.houseFloor = reader.ReadElementContentAsInt();
            inst.apartNum = reader.ReadElementContentAsInt();
            inst.numOfRooms = reader.ReadElementContentAsInt();
            inst.Description = reader.ReadElementContentAsString();
            int ownerId = reader.ReadElementContentAsInt();
            inst.Owner = dataSet.Owners.FirstOrDefault(e => e.Id == ownerId);
            dataSet.Apartaments.Add(inst);
        }

        //------------------------------OPERATIONS--------------------------------------OPERATIONS----------------------------------------------------OPERATIONS--------------------------------------------------- 

        public void Save(IDataSet dataSet, string filePath)
        {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(filePath, settings);
                WriteData(dataSet, writer);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if(writer != null)
                {
                    writer.Close();
                }
            }
        }

        public bool Load(IDataSet dataSet, string filePath)
        {
            Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath)) return false;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                while(reader.Read()) 
                {
                    if(reader.NodeType == XmlNodeType.Element)
                    {
                        switch(reader.Name)
                        {
                            case "Owner":
                                ReadOwner(reader, dataSet.Owners);
                                break;
                            case "Apartament":
                                ReadApartament(reader, dataSet);
                                break;
                        }
                    }
                }
            }
            return true;
        }
    }
}
