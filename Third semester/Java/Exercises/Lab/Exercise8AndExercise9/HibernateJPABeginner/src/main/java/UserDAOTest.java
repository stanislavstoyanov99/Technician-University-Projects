import net.codejava.hibernate.models.User;
import org.hibernate.Session;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.Query;

public class UserDAOTest {
    public static void main(String[] args) {
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("UsersDB");
        EntityManager entityManager = factory.createEntityManager();

        entityManager.getTransaction().begin();

        /* Creates user and adds it to the database
        User newUser = new User();
        newUser.setEmail("billjoy@gmail.com");
        newUser.setFullname("Bill Joy");
        newUser.setPassword("billi");

        entityManager.persist(newUser);
        entityManager.getTransaction().commit();
         */

        /* Updates existing user
        User existingUser = new User();

        existingUser.setId(1);
        existingUser.setEmail("bill.joy@gmail.com");
        existingUser.setFullname("Bill Joy");
        existingUser.setPassword("billionaire");

        entityManager.merge(existingUser);
         */

        /* Find an entity instance
        Integer primaryKey = 1;
        User user = entityManager.find(User.class, primaryKey);

        System.out.println(user.getEmail());
        System.out.println(user.getFullname());
        System.out.println(user.getPassword());
        */

        /* Execute a query
        String sql = "SELECT u from User u where u.email = 'bill.joy@gmail.com'";
        Query query = entityManager.createQuery(sql);
        User foundUser = (User) query.getSingleResult();

        System.out.println(foundUser.getEmail());
        System.out.println(foundUser.getFullname());
        System.out.println(foundUser.getPassword());
        */

        /* Remove an entity instance
        Integer primaryKey = 1;
        User reference = entityManager.getReference(User.class, primaryKey);
        entityManager.remove(reference);
         */

        entityManager.getTransaction().commit();
        entityManager.close();
        factory.close();
    }
}
