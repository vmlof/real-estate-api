using Homely.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homely.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMigration : Migration
    {
        // TODO: gerar UMA vez com o hasher real da aplicação (senha de desenvolvimento) e colar aqui.
        private const string PasswordHash = "PASSWORD_HASH";
        
        private const int RoleManager = (int)Role.Manager;
        private const int RoleTenant = (int)Role.Tenant;
        
        private const int AppPending = (int)ApplicationStatus.Pending;
        private const int AppApproved = (int)ApplicationStatus.Approved;
        private const int AppDenied = (int)ApplicationStatus.Denied;
        
        private const int PayPending = (int)PaymentStatus.Pending;
        private const int PayPaid = (int)PaymentStatus.Paid;
        private const int PayPartial = (int)PaymentStatus.PartiallyPaid;
        
        private const int TypeApartment = (int)PropertyType.Apartment;
        private const int TypeVilla = (int)PropertyType.Villa;
        private const int TypeTownHouse = (int)PropertyType.TownHouse;
        private const int TypeCottage = (int)PropertyType.Cottage;
        private const int TypeTinyHouse = (int)PropertyType.TinyHouse;
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql($"""
                INSERT INTO users (id, email, password_hash, role) VALUES
                (1,  'john.smith@example.com',      '{PasswordHash}', {RoleManager}),
                (2,  'sarah.johnson@example.com',   '{PasswordHash}', {RoleManager}),
                (3,  'michael.brown@example.com',   '{PasswordHash}', {RoleManager}),
                (4,  'emily.davis@example.com',     '{PasswordHash}', {RoleManager}),
                (5,  'robert.wilson@example.com',   '{PasswordHash}', {RoleManager}),
                (6,  'lisa.thompson@example.com',   '{PasswordHash}', {RoleManager}),
                (7,  'daniel.martinez@example.com', '{PasswordHash}', {RoleManager}),
                (8,  'olivia.garcia@example.com',   '{PasswordHash}', {RoleManager}),
                (9,  'ethan.rodriguez@example.com', '{PasswordHash}', {RoleManager}),
                (10, 'sophia.kim@example.com',      '{PasswordHash}', {RoleManager}),
                (11, 'carol.white@example.com',     '{PasswordHash}', {RoleTenant}),
                (12, 'bob.green@example.com',       '{PasswordHash}', {RoleTenant}),
                (13, 'alice.brown@example.com',     '{PasswordHash}', {RoleTenant}),
                (14, 'david.lee@example.com',       '{PasswordHash}', {RoleTenant}),
                (15, 'emma.taylor@example.com',     '{PasswordHash}', {RoleTenant}),
                (16, 'frank.wilson@example.com',    '{PasswordHash}', {RoleTenant}),
                (17, 'grace.miller@example.com',    '{PasswordHash}', {RoleTenant}),
                (18, 'henry.wilson@example.com',    '{PasswordHash}', {RoleTenant}),
                (19, 'isabella.garcia@example.com', '{PasswordHash}', {RoleTenant}),
                (20, 'jack.thompson@example.com',   '{PasswordHash}', {RoleTenant}),
                (21, 'karen.martinez@example.com',  '{PasswordHash}', {RoleTenant}),
                (22, 'liam.johnson@example.com',    '{PasswordHash}', {RoleTenant}),
                (23, 'mia.rodriguez@example.com',   '{PasswordHash}', {RoleTenant}),
                (24, 'noah.kim@example.com',        '{PasswordHash}', {RoleTenant}),
                (25, 'olivia.chen@example.com',     '{PasswordHash}', {RoleTenant});
                """);

            // managers
            mb.Sql("""
                INSERT INTO managers (id, user_id, name, phone_number) VALUES
                (1,  1,  'John Smith',      '+1 (555) 123-4567'),
                (2,  2,  'Sarah Johnson',   '+1 (555) 987-6543'),
                (3,  3,  'Michael Brown',   '+1 (555) 456-7890'),
                (4,  4,  'Emily Davis',     '+1 (555) 234-5678'),
                (5,  5,  'Robert Wilson',   '+1 (555) 876-5432'),
                (6,  6,  'Lisa Thompson',   '+1 (555) 345-6789'),
                (7,  7,  'Daniel Martinez', '+1 (555) 789-0123'),
                (8,  8,  'Olivia Garcia',   '+1 (555) 678-9012'),
                (9,  9,  'Ethan Rodriguez', '+1 (555) 567-8901'),
                (10, 10, 'Sophia Kim',      '+1 (555) 456-7890');
                """);

            // tenants (user_id = id + 10)
            mb.Sql("""
                INSERT INTO tenants (id, user_id, name, phone_number) VALUES
                (1,  11, 'Carol White',     '+1 (555) 555-6666'),
                (2,  12, 'Bob Green',       '+1 (555) 333-4444'),
                (3,  13, 'Alice Brown',     '+1 (555) 111-2222'),
                (4,  14, 'David Lee',       '+1 (555) 777-8888'),
                (5,  15, 'Emma Taylor',     '+1 (555) 999-0000'),
                (6,  16, 'Frank Wilson',    '+1 (555) 222-3333'),
                (7,  17, 'Grace Miller',    '+1 (555) 444-5555'),
                (8,  18, 'Henry Wilson',    '+1 (555) 444-5555'),
                (9,  19, 'Isabella Garcia', '+1 (555) 666-7777'),
                (10, 20, 'Jack Thompson',   '+1 (555) 888-9999'),
                (11, 21, 'Karen Martinez',  '+1 (555) 123-4567'),
                (12, 22, 'Liam Johnson',    '+1 (555) 987-6543'),
                (13, 23, 'Mia Rodriguez',   '+1 (555) 246-8135'),
                (14, 24, 'Noah Kim',        '+1 (555) 369-2580'),
                (15, 25, 'Olivia Chen',     '+1 (555) 159-7531');
                """);

            // locations (WKT é POINT(lon lat); aqui já separado em latitude/longitude)
            mb.Sql("""
                INSERT INTO locations (id, address, city, state, country, postal_code, latitude, longitude) VALUES
                (1,  '123 Colorado Blvd',   'Pasadena',      'CA', 'United States', '91105', 34.147785,  -118.144516),
                (2,  '456 Ocean Ave',       'Santa Monica',  'CA', 'United States', '90401', 34.013654,  -118.496513),
                (3,  '789 Hollywood Way',   'Burbank',       'CA', 'United States', '91505', 34.180839,  -118.328661),
                (4,  '101 Pine Ave',        'Long Beach',    'CA', 'United States', '90802', 33.766720,  -118.192604),
                (5,  '555 Manhattan Ave',   'New York',      'NY', 'United States', '10001', 40.712784,  -74.005941),
                (6,  '888 Malibu Road',     'Malibu',        'CA', 'United States', '90265', 34.025922,  -118.774518),
                (7,  '777 Brand Blvd',      'Glendale',      'CA', 'United States', '91203', 34.142508,  -118.254708),
                (8,  '555 Torrance Blvd',   'Torrance',      'CA', 'United States', '90503', 33.835849,  -118.352575),
                (9,  '1234 Ocean Dr',       'Miami',         'FL', 'United States', '33139', 25.782551,  -80.130045),
                (10, '789 Rodeo Dr',        'Beverly Hills', 'CA', 'United States', '90210', 34.073620,  -118.400356);
                """);

            // ───────────── properties ─────────────
            // Os rótulos de amenity/highlight seguem o snake_case padrão do Npgsql.
            // CONFIRA contra o CREATE TYPE (ou "Npgsql:Enum:amenity"/"highlight") da migration inicial.
            mb.Sql($"""
                INSERT INTO properties
                    (id, name, description, price_per_month, security_deposit, application_fee,
                     photo_urls, amenities, highlights, is_pets_allowed, is_parking_included,
                     beds, baths, square_feet, property_type, posted_date,
                     average_rating, number_of_reviews, location_id, manager_id)
                VALUES
                (1, 'Sunny Downtown Apartment',
                    'A beautiful apartment in the heart of downtown with plenty of natural light.',
                    1500.00, 1500.00, 50.00,
                    ARRAY['https://example.com/apartment1_1.jpg','https://example.com/apartment1_2.jpg']::text[],
                    ARRAY['air_conditioning','washer_dryer','parking']::amenity[],
                    ARRAY['high_speed_internet_access','close_to_transit']::highlight[],
                    true, false, 2, 1, 800, {TypeApartment}, '2023-05-15T00:00:00Z', 4.5, 10, 1, 1),

                (2, 'Cozy Beach House',
                    'A charming beach house with stunning ocean views.',
                    2000.00, 2000.00, 75.00,
                    ARRAY['https://example.com/beachhouse1.jpg','https://example.com/beachhouse2.jpg']::text[],
                    ARRAY['air_conditioning','washer_dryer','parking','pool']::amenity[],
                    ARRAY['great_view','close_to_transit']::highlight[],
                    true, true, 3, 2, 1200, {TypeVilla}, '2023-06-01T00:00:00Z', 4.8, 5, 2, 2),

                (3, 'Modern City Loft',
                    'Sleek and stylish loft in the heart of the city.',
                    2200.00, 2200.00, 60.00,
                    ARRAY['https://example.com/cityloft1.jpg','https://example.com/cityloft2.jpg']::text[],
                    ARRAY['air_conditioning','washer_dryer','gym']::amenity[],
                    ARRAY['high_speed_internet_access','close_to_transit']::highlight[],
                    true, false, 1, 1, 900, {TypeApartment}, '2023-07-01T00:00:00Z', 4.7, 8, 3, 3),

                (4, 'Spacious Family Home',
                    'Large family home with a beautiful backyard and modern amenities.',
                    2500.00, 2500.00, 80.00,
                    ARRAY['https://example.com/familyhome1.jpg','https://example.com/familyhome2.jpg']::text[],
                    ARRAY['air_conditioning','washer_dryer','parking','dishwasher']::amenity[],
                    ARRAY['quiet_neighborhood']::highlight[],
                    true, false, 4, 3, 2000, {TypeVilla}, '2023-06-15T00:00:00Z', 4.9, 12, 4, 4),

                (5, 'Luxury Penthouse',
                    'Stunning penthouse with panoramic city views and high-end finishes.',
                    5000.00, 5000.00, 100.00,
                    ARRAY['https://example.com/penthouse1.jpg','https://example.com/penthouse2.jpg']::text[],
                    ARRAY['air_conditioning','washer_dryer','gym','pool']::amenity[],
                    ARRAY['great_view']::highlight[],
                    true, false, 3, 3, 2500, {TypeApartment}, '2023-07-01T00:00:00Z', 5.0, 15, 5, 5),

                (6, 'Cozy Studio Apartment',
                    'Efficient studio apartment perfect for students or young professionals.',
                    1200.00, 1200.00, 40.00,
                    ARRAY['https://example.com/studio1.jpg','https://example.com/studio2.jpg']::text[],
                    ARRAY['air_conditioning','high_speed_internet']::amenity[],
                    ARRAY['close_to_transit']::highlight[],
                    true, false, 0, 1, 400, {TypeApartment}, '2023-08-01T00:00:00Z', 4.2, 6, 6, 6),

                (7, 'Historic Brownstone',
                    'Charming brownstone with original features and modern updates.',
                    3000.00, 3000.00, 70.00,
                    ARRAY['https://example.com/brownstone1.jpg','https://example.com/brownstone2.jpg']::text[],
                    ARRAY['air_conditioning','washer_dryer','high_speed_internet']::amenity[],
                    ARRAY['recently_renovated']::highlight[],
                    true, false, 3, 2, 1800, {TypeTownHouse}, '2023-09-01T00:00:00Z', 4.6, 9, 7, 7),

                (8, 'Urban Micro-Apartment',
                    'Compact and efficient living space in the heart of the city.',
                    1000.00, 1000.00, 30.00,
                    ARRAY['https://example.com/micro1.jpg','https://example.com/micro2.jpg']::text[],
                    ARRAY['air_conditioning','high_speed_internet']::amenity[],
                    ARRAY['close_to_transit']::highlight[],
                    true, false, 0, 1, 300, {TypeApartment}, '2023-08-01T00:00:00Z', 4.3, 7, 8, 8),

                (9, 'Mountain View Cabin',
                    'Rustic cabin with breathtaking mountain views and modern amenities.',
                    1800.00, 1800.00, 60.00,
                    ARRAY['https://example.com/cabin1.jpg','https://example.com/cabin2.jpg']::text[],
                    ARRAY['washer_dryer','air_conditioning']::amenity[],
                    ARRAY['great_view','quiet_neighborhood']::highlight[],
                    true, false, 2, 1, 1000, {TypeCottage}, '2023-08-15T00:00:00Z', 4.9, 11, 9, 9),

                (10, 'Eco-Friendly Tiny House',
                    'Sustainable living in a compact, well-designed tiny house.',
                    900.00, 900.00, 35.00,
                    ARRAY['https://example.com/tinyhouse1.jpg','https://example.com/tinyhouse2.jpg']::text[],
                    ARRAY['washer_dryer','air_conditioning','high_speed_internet']::amenity[],
                    ARRAY['smoke_free']::highlight[],
                    true, false, 1, 1, 250, {TypeTinyHouse}, '2023-08-10T00:00:00Z', 4.7, 8, 10, 10);
                """);

            // leases
            mb.Sql("""
                INSERT INTO leases (id, start_date, end_date, rent, deposit, property_id, tenant_id) VALUES
                (1,  '2023-07-01T00:00:00Z', '2024-06-30T00:00:00Z', 1500.00, 1500.00, 1,  3),
                (2,  '2023-08-01T00:00:00Z', '2024-07-31T00:00:00Z', 1800.00, 1800.00, 2,  2),
                (3,  '2023-09-01T00:00:00Z', '2024-08-31T00:00:00Z', 2200.00, 2200.00, 3,  1),
                (4,  '2023-07-15T00:00:00Z', '2024-07-14T00:00:00Z', 1700.00, 1700.00, 4,  4),
                (5,  '2023-08-15T00:00:00Z', '2024-08-14T00:00:00Z', 2000.00, 2000.00, 5,  5),
                (6,  '2023-09-15T00:00:00Z', '2024-09-14T00:00:00Z', 2400.00, 2400.00, 6,  6),
                (7,  '2023-10-01T00:00:00Z', '2024-09-30T00:00:00Z', 2200.00, 2200.00, 3,  7),
                (8,  '2023-08-01T00:00:00Z', '2024-07-31T00:00:00Z', 5000.00, 5000.00, 5,  8),
                (9,  '2023-11-01T00:00:00Z', '2024-10-31T00:00:00Z', 3000.00, 3000.00, 7,  9),
                (10, '2023-08-15T00:00:00Z', '2024-02-14T00:00:00Z', 2000.00, 2000.00, 2,  10),
                (11, '2023-09-01T00:00:00Z', '2024-08-31T00:00:00Z', 1000.00, 1000.00, 8,  11),
                (12, '2023-10-01T00:00:00Z', '2023-12-31T00:00:00Z', 1800.00, 1800.00, 9,  12),
                (13, '2023-09-15T00:00:00Z', '2024-03-14T00:00:00Z',  900.00,  900.00, 10, 13),
                (14, '2023-09-01T00:00:00Z', '2024-08-31T00:00:00Z', 1500.00, 1500.00, 1,  14),
                (15, '2023-10-01T00:00:00Z', '2024-09-30T00:00:00Z', 2500.00, 2500.00, 4,  15);
                """);

            // applications
            mb.Sql($"""
                INSERT INTO applications
                    (id, application_date, status, property_id, tenant_id, name, email, phone_number, message, lease_id)
                VALUES
                (1,  '2023-05-20T00:00:00Z', {AppApproved}, 1,  3,  'Alice Brown',     'alice.brown@example.com',     '+1 (555) 111-2222', 'I am interested in this property.', 1),
                (2,  '2023-05-25T00:00:00Z', {AppPending},  2,  2,  'Bob Green',       'bob.green@example.com',       '+1 (555) 333-4444', 'Looking forward to viewing this apartment.', NULL),
                (3,  '2023-06-01T00:00:00Z', {AppDenied},   3,  1,  'Carol White',     'carol.white@example.com',     '+1 (555) 555-6666', 'Excited about the possibility of renting this house.', NULL),
                (4,  '2023-06-10T00:00:00Z', {AppApproved}, 4,  4,  'David Lee',       'david.lee@example.com',       '+1 (555) 777-8888', 'This property looks perfect for my needs.', 4),
                (5,  '2023-06-15T00:00:00Z', {AppPending},  5,  5,  'Emma Taylor',     'emma.taylor@example.com',     '+1 (555) 999-0000', 'I''m very interested in this apartment.', NULL),
                (6,  '2023-06-20T00:00:00Z', {AppApproved}, 6,  6,  'Frank Wilson',    'frank.wilson@example.com',    '+1 (555) 222-3333', 'This property seems to fit all my requirements.', 6),
                (7,  '2023-06-25T00:00:00Z', {AppPending},  3,  7,  'Grace Miller',    'grace.miller@example.com',    '+1 (555) 444-5555', 'I''m interested in this loft and would like to schedule a viewing.', NULL),
                (8,  '2023-07-01T00:00:00Z', {AppApproved}, 5,  8,  'Henry Wilson',    'henry.wilson@example.com',    '+1 (555) 444-5555', 'I''m very interested in this luxury penthouse.', 8),
                (9,  '2023-07-05T00:00:00Z', {AppPending},  7,  9,  'Isabella Garcia', 'isabella.garcia@example.com', '+1 (555) 666-7777', 'The historic brownstone looks perfect for my family.', NULL),
                (10, '2023-07-10T00:00:00Z', {AppDenied},   2,  10, 'Jack Thompson',   'jack.thompson@example.com',   '+1 (555) 888-9999', 'I''d love to rent this beach house for the summer.', NULL),
                (11, '2023-07-15T00:00:00Z', {AppPending},  8,  11, 'Karen Martinez',  'karen.martinez@example.com',  '+1 (555) 123-4567', 'I''m interested in this micro-apartment for its central location.', NULL),
                (12, '2023-07-20T00:00:00Z', {AppApproved}, 9,  12, 'Liam Johnson',    'liam.johnson@example.com',    '+1 (555) 987-6543', 'The mountain view cabin looks perfect for a weekend getaway.', 12),
                (13, '2023-07-25T00:00:00Z', {AppPending},  10, 13, 'Mia Rodriguez',   'mia.rodriguez@example.com',   '+1 (555) 246-8135', 'I''m curious about the eco-friendly features of this tiny house.', NULL),
                (14, '2023-07-30T00:00:00Z', {AppApproved}, 1,  14, 'Noah Kim',        'noah.kim@example.com',        '+1 (555) 369-2580', 'The downtown apartment seems ideal for my work location.', 14),
                (15, '2023-08-05T00:00:00Z', {AppPending},  4,  15, 'Olivia Chen',     'olivia.chen@example.com',     '+1 (555) 159-7531', 'I''m looking for a spacious family home like this one.', NULL);
                """);

            // payments
            // Ajustes em relação ao JSON original:
            //  • ids 2, 5 e 11 (Pending): payment_date = NULL (antes tinham data de pagamento);
            //  • id 15: amount_paid = 2500 (antes 0.0 com status Paid).
            mb.Sql($"""
                INSERT INTO payments
                    (id, amount_due, amount_paid, due_date, payment_date, payment_status, lease_id)
                VALUES
                (1,  1500.00, 1500.00, '2023-07-01T00:00:00Z', '2023-06-28T00:00:00Z', {PayPaid},    1),
                (2,  1800.00,    0.00, '2023-08-01T00:00:00Z', NULL,                   {PayPending}, 2),
                (3,  2200.00, 2200.00, '2023-09-01T00:00:00Z', '2023-08-30T00:00:00Z', {PayPaid},    3),
                (4,  1700.00, 1700.00, '2023-07-15T00:00:00Z', '2023-07-14T00:00:00Z', {PayPaid},    4),
                (5,  2000.00,    0.00, '2023-08-15T00:00:00Z', NULL,                   {PayPending}, 5),
                (6,  2400.00, 2400.00, '2023-09-15T00:00:00Z', '2023-09-14T00:00:00Z', {PayPaid},    6),
                (7,  2200.00, 1100.00, '2023-10-01T00:00:00Z', '2023-09-25T00:00:00Z', {PayPartial}, 7),
                (8,  5000.00, 5000.00, '2023-08-01T00:00:00Z', '2023-07-30T00:00:00Z', {PayPaid},    8),
                (9,  3000.00, 1500.00, '2023-11-01T00:00:00Z', '2023-10-25T00:00:00Z', {PayPartial}, 9),
                (10, 2000.00, 2000.00, '2023-08-15T00:00:00Z', '2023-08-10T00:00:00Z', {PayPaid},    10),
                (11, 1000.00,    0.00, '2023-09-01T00:00:00Z', NULL,                   {PayPending}, 11),
                (12, 1800.00, 1800.00, '2023-10-01T00:00:00Z', '2023-09-28T00:00:00Z', {PayPaid},    12),
                (13,  900.00,  450.00, '2023-09-15T00:00:00Z', '2023-09-10T00:00:00Z', {PayPartial}, 13),
                (14, 1500.00, 1500.00, '2023-09-01T00:00:00Z', '2023-08-30T00:00:00Z', {PayPaid},    14),
                (15, 2500.00, 2500.00, '2023-10-01T00:00:00Z', '2023-09-28T00:00:00Z', {PayPaid},    15);
                """);

            // favorite_properties
            mb.Sql("""
                INSERT INTO favorite_properties (tenant_id, property_id) VALUES
                (2, 2),
                (4, 4), (4, 5),
                (5, 1), (5, 5), (5, 6),
                (7, 3), (7, 7),
                (9, 7), (9, 9),
                (10, 2), (10, 10),
                (11, 1), (11, 4), (11, 7),
                (12, 3), (12, 6), (12, 9),
                (14, 1), (14, 10),
                (15, 4), (15, 7), (15, 9);
                """);

            // sequences: ids explícitos não avançam o identity do PostgreSQL 
            mb.Sql("""
                SELECT setval(pg_get_serial_sequence('users',        'id'), (SELECT MAX(id) FROM users));
                SELECT setval(pg_get_serial_sequence('managers',     'id'), (SELECT MAX(id) FROM managers));
                SELECT setval(pg_get_serial_sequence('tenants',      'id'), (SELECT MAX(id) FROM tenants));
                SELECT setval(pg_get_serial_sequence('locations',    'id'), (SELECT MAX(id) FROM locations));
                SELECT setval(pg_get_serial_sequence('properties',   'id'), (SELECT MAX(id) FROM properties));
                SELECT setval(pg_get_serial_sequence('leases',       'id'), (SELECT MAX(id) FROM leases));
                SELECT setval(pg_get_serial_sequence('applications', 'id'), (SELECT MAX(id) FROM applications));
                SELECT setval(pg_get_serial_sequence('payments',     'id'), (SELECT MAX(id) FROM payments));
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("""
                                 DELETE FROM favorite_properties
                                 WHERE (tenant_id, property_id) IN (
                                     (2, 2),
                                     (4, 4), (4, 5),
                                     (5, 1), (5, 5), (5, 6),
                                     (7, 3), (7, 7),
                                     (9, 7), (9, 9),
                                     (10, 2), (10, 10),
                                     (11, 1), (11, 4), (11, 7),
                                     (12, 3), (12, 6), (12, 9),
                                     (14, 1), (14, 10),
                                     (15, 4), (15, 7), (15, 9)
                                 );
                                 """);

            mb.Sql("DELETE FROM payments     WHERE id BETWEEN 1 AND 15;");
            mb.Sql("DELETE FROM applications WHERE id BETWEEN 1 AND 15;");
            mb.Sql("DELETE FROM leases       WHERE id BETWEEN 1 AND 15;");
            mb.Sql("DELETE FROM properties   WHERE id BETWEEN 1 AND 10;");
            mb.Sql("DELETE FROM locations    WHERE id BETWEEN 1 AND 10;");
            mb.Sql("DELETE FROM tenants      WHERE id BETWEEN 1 AND 15;");
            mb.Sql("DELETE FROM managers     WHERE id BETWEEN 1 AND 10;");
            mb.Sql("DELETE FROM users        WHERE id BETWEEN 1 AND 25;");
        }
    }
}
