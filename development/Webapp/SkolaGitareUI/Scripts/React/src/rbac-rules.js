const rules = {
    visitor: {
        static: [
        ]
    },
    Teacher: {
        static: [
            "lessons:visit",
            "reservations:visit",
            "users:visit",
            "teacher-lessons:visit"
        ]
    },
    Student: {
        static: [
            "lessons:visit",
            "tariff:edit",
            "student-lessons:visit"
        ]
    }, 
    Admin: {
        static: [
            "reservations:visit",
            "users:visit",
            "admin-lessons:visit",
            "students:delete",
            "teachers:delete",
            
        ]
    }
}
export default rules;