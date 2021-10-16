using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityInCore3.DAL.Models
{
    public abstract class DomainEntity : IEquatable<DomainEntity>
    {
        #region Properties

        /// <summary>
        /// DomainProcessor Object Identifier
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// The time when the content is created
        /// </summary>
        public virtual DateTimeOffset? CreatedOn { get; set; }

        

        /// <summary>
        /// ModifiedOn property for Concurrency control
        /// </summary>
        public virtual DateTimeOffset? ModifiedOn { get; set; }

       

        #endregion
        #region Public Methods

        /// <summary>
        /// Checks whether the entity is new 
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        public virtual bool IsNew()
        {
            return String.IsNullOrEmpty(this.Id.ToString());
        }


        #endregion

        #region Overrides Methods

        /// <summary>
        /// <see cref="M:System.Object.Equals"/>
        /// </summary>
        /// <param name="obj"><see cref="M:System.Object.Equals"/></param>
        /// <returns><see cref="M:System.Object.Equals"/></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainEntity))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            DomainEntity item = (DomainEntity)obj;

            if (item.IsNew() || this.IsNew())
            {
                return false;
            }
            else
            {
                return item.Id == this.Id;
            }
            // return true;
        }

        /// <summary>
        /// <see cref="M:System.Object.GetHashCode"/>
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode"/></returns>
        int? requestedHashCode;
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {

            if (!IsNew())
            {
                if (!requestedHashCode.HasValue)
                {
                    requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution 
                }
                return requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
            // return 0;
        }

        /// <summary>
        /// ==s the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator ==(DomainEntity left, DomainEntity right)
        {
            if (Object.Equals(left, null))
            {
                return (Object.Equals(right, null)) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// !=s the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static bool operator !=(DomainEntity left, DomainEntity right)
        {
            return !(left == right);
        }

        #endregion

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public virtual bool Equals(DomainEntity other)
        {
            return this.Equals((object)other);
        }
    }
}
