using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CandidateDataAccess;
namespace WebApplication4.Controllers
{
    public class CandidateController : ApiController
    {
        public IEnumerable<Candidate> Get()
        {
            using (DBEntities entities = new DBEntities())
            {
                return entities.Candidates.ToList();
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (DBEntities entities = new DBEntities())
            {
                var entity = entities.Candidates.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Candidate with id = " + id.ToString() + " Not Found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Candidate c)
        {
            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    entities.Candidates.Add(c);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, c);
                    message.Headers.Location = new Uri(Request.RequestUri + c.ID.ToString());
                    return message;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    var entity = entities.Candidates.FirstOrDefault(c => c.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Candidate with id = " + id.ToString() + " Not Found");
                    }
                    else
                    {
                        entities.Candidates.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e) ;
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Candidate c)
        {
            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    var entity = entities.Candidates.FirstOrDefault(can => can.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Candidate with id = " + id.ToString() + " Not Found to update");
                    }
                    else
                    {
                        entity.Password1 = c.Password1;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
